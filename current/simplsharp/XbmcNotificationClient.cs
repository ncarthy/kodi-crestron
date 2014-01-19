using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;

namespace XbmcTcpSocket
{
    public class XbmcNotificationClient : XbmcClient
    {
        #region Private Fields
        private TCPClient client = null;
        #endregion

        public void StartClient()
        {

            // Connect to a remote device.
            try
            {
                if (IPAddress != null && IPAddress != "")
                {
                    client = new TCPClient(IPAddress, (int)port, bufferSize);
                }
                else
                {
                    // If an error is thrown, and the OnError delegate has been subscribed to, the delegate is called.
                    if (OnError != null)
                        OnError(new SimplSharpString(string.Format("InvalidCastException IPAddress ({0})", IPAddress)));
                }

                if (client != null && client.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    // Connect to the remote endpoint.
                    client.ConnectToServerAsync(ConnectCallback);
                }
                else
                {
                    OnError(new SimplSharpString("Something has gone wrong. Client is NOT listening."));
                }
            }
            catch (Exception e)
            {
                // If an error is thrown, and the OnError delegate has been subscribed to, the delegate is called.
                if (OnError != null)
                    OnError(new SimplSharpString(e.ToString() + "\n\r" + e.StackTrace));
            }
        }

        public void StopClient()
        {
            try
            {
                // Release the socket.
                if (client != null && client.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    if (client.DisconnectFromServer() == SocketErrorCodes.SOCKET_OK)
                    {
                        OnDisconnected(new EventArgs());
                    }
                    else
                    {
                        if (OnError != null)
                            OnError(new SimplSharpString("Error disconnecting from Socket"));
                    }
                }
            }
            catch (Exception e)
            {
                // If an error is thrown, and the OnError delegate has been subscribed to, the delegate is called.
                if (OnError != null)
                    OnError(new SimplSharpString(e.ToString() + "\n\r" + e.StackTrace));
            }
        }

        private void ReceiveCallback(TCPClient myTCPClient, int numberOfBytesReceived)
        {
            try
            {
                if (numberOfBytesReceived > 0)
                {
                    OnMessage(new SimplSharpString(string.Format("{0} bytes received.", numberOfBytesReceived)));

                    state.bytesReceived = (ushort)numberOfBytesReceived;
                    var receivedJsonData = Encoding.UTF8.GetString(myTCPClient.IncomingDataBuffer, 0, numberOfBytesReceived);
                    state.data = new SimplSharpString(receivedJsonData);

                    // Send Data SIMPL+ module via an event
                    OnDataReceived(new ReceiveEventArgs(state));

                    // Reset state object
                    state = new StateObject();
                }

                // Continue listening, with a new state object.
                if (client != null && client.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    client.ReceiveDataAsync(ReceiveCallback);
                }
            }
            catch (Exception e)
            {
                // If an error is thrown, and the OnError delegate has been subscribed to, the delegate is called.
                if (OnError != null)
                    OnError(new SimplSharpString(e.ToString() + "\n\r" + e.StackTrace));
            }
        }

        private void ConnectCallback(TCPClient myTCPClient)
        {
            try
            {
                if (client != null && client.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    // Tell the world
                    OnMessage(new SimplSharpString("SIMPL# socket is connected."));
                    OnConnected(new EventArgs());

                    // Start listening
                    client.ReceiveDataAsync(ReceiveCallback);
                }
            }
            catch (Exception e)
            {
                // If an error is thrown, and the OnError delegate has been subscribed to, the delegate is called.
                if (OnError != null)
                    OnError(new SimplSharpString(e.ToString() + "\n\r" + e.StackTrace));
            }
        }

    }
}