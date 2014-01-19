using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace XbmcTcpSocket
{
    public abstract class XbmcClient : ISimplePlusBroadcaster
    {
        #region Protected Fields
        protected ushort port;
        protected int bufferSize = 32767;
        protected StateObject state = new StateObject();
        #endregion

        #region Public Properties

        public string IPAddress { get; set; }

        public ushort Port
        {
            get { return port; }
            set { port = value; }
        }
        #endregion

        #region Events & Delegates
        public errorHandler OnError { get; set; }
        //public delegate void errorHandler(SimplSharpString errMsg);
        public messageHandler OnMessage { get; set; }
        //public delegate void messageHandler(SimplSharpString msg); 

        /// <summary>
        /// Occurs when the client is connected.
        /// </summary>
        public event EventHandler Connected = delegate { };
        public event EventHandler Disconnected = delegate { };

        /// <summary>
        /// Occurs when text is received.
        /// </summary>
        public event ReceiveEventHandler DataReceived = delegate { };

        public delegate void ReceiveEventHandler(object sender, ReceiveEventArgs e);

        /// <summary>
        /// Raises the Connected event
        /// </summary>
        /// <param name="e">AnEventArgs that contains the event data.</param>
        protected virtual void OnConnected(EventArgs e)
        {
            //Raise the event.
            Connected(this, e);
        }
        /// <summary>
        /// Raises the Connected event
        /// </summary>
        /// <param name="e">AnEventArgs that contains the event data.</param>
        protected virtual void OnDisconnected(EventArgs e)
        {
            //Raise the event.
            Disconnected(this, e);
        }
        /// <summary>
        /// Raises the DataReceived event
        /// </summary>
        /// <param name="e">AnEventArgs that contains the event data.</param>
        protected virtual void OnDataReceived(ReceiveEventArgs e)
        {
            //Raise the event.
            DataReceived(this, e);
        }
        #endregion
    }
}