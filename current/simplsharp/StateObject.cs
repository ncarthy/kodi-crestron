using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace XbmcTcpSocket
{
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Receive buffer.
        public SimplSharpString data = null;

        // Number of Bytes Received.
        public ushort bytesReceived { get; set; }

    }
}