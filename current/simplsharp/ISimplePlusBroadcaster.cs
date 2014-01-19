using Crestron.SimplSharp;

namespace XbmcTcpSocket
{
    public delegate void errorHandler(SimplSharpString errMsg);
    public delegate void messageHandler(SimplSharpString msg);

    public interface ISimplePlusBroadcaster
    {
        errorHandler OnError { get; set; }
        messageHandler OnMessage { get; set; }
    }
}