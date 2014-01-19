using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace XbmcTcpSocket
{
    public class ReceiveEventArgs : EventArgs
    {
        public StateObject state = null;

        public ReceiveEventArgs() : base() { }

        public ReceiveEventArgs(StateObject state)
            : base()
        {
            this.state = state;
        }
    }
}