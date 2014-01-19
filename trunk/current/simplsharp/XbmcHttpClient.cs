using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net;
using Crestron.SimplSharp.Net.Http;

namespace XbmcTcpSocket
{
    public class XbmcHttpClient : XbmcClient
    {
        #region Private Fields
        private HttpClient httpClient = null;
        private string url = null;
        #endregion

        public int Timeout { get; set; }

        public void Send(string jsonrpc)
        {
            httpClient = new HttpClient();
            if (Timeout > 0)
            {
                httpClient.Timeout = Timeout;
                httpClient.TimeoutEnabled = true;
            }

            HttpClientRequest httpRequest = new HttpClientRequest();
            HttpClientResponse httpResponse;

            httpClient.KeepAlive = false;
            httpRequest.Url.Parse(GetUrl());
            httpRequest.Encoding = Encoding.UTF8;
            //httpRequest.Encoding = Encoding.Unicode;
            httpRequest.Header.ContentType = "application/json";
            httpRequest.RequestType = RequestType.Post;
            httpRequest.ContentBytes = Encoding.ASCII.GetBytes(jsonrpc);
            httpRequest.ContentSource = ContentSource.ContentBytes;

            using (httpResponse = httpClient.Dispatch(httpRequest))
            {
                // Reset state object
                state = new StateObject();

                //Making big assumption that content is less than 65535 bytes
                if (httpResponse.HasContentLength)
                    state.bytesReceived = (ushort)httpResponse.ContentLength;
                else
                    state.bytesReceived = (ushort)httpResponse.ContentString.Length;
                
                var receivedJsonData = Encoding.UTF8.GetString(httpResponse.ContentBytes, 0, state.bytesReceived);
                state.data = new SimplSharpString(receivedJsonData);

                //OnMessage(new SimplSharpString(string.Format("bytes: {0}", state.bytesReceived)));
                //OnMessage(new SimplSharpString(string.Format("data: {0}", receivedJsonData.Substring(0, Math.Min((ushort)100, state.bytesReceived)))));                

                OnDataReceived(new ReceiveEventArgs(state));
                
            }
        }

        private string GetUrl()
        {
            if (url == null || url == "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"http://");
                if (IPAddress == null || IPAddress == "")
                    OnError(new SimplSharpString("Invalid or empty IPAddress"));
                else
                    sb.Append(IPAddress);
                sb.Append(":");
                sb.Append(port.ToString());
                sb.Append("/jsonrpc");
                url = sb.ToString();
            }

            return url;
        }
    }
}