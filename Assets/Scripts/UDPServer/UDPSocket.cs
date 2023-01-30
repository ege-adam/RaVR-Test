using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


public class UDPSocket
{
    public delegate void OnReceived(Packet packet);
    private const int bufferSize = 128;

    private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
    private Status state = new Status();
    private AsyncCallback receive = null;
    private OnReceived onReceived;

    public class Status
    {
        public byte[] buffer = new byte[bufferSize];
    }

    public void Server(string address, int port, OnReceived _onReceived)
    {
        onReceived = _onReceived;

        socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
        socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
        Receive();
    }

    public void Client(string address, int port)
    {
        socket.Connect(IPAddress.Parse(address), port);
    }

    public void Send(string text)
    {
        byte[] data = Encoding.ASCII.GetBytes(text);
        socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
        {
            Status so = (Status)ar.AsyncState;
            int bytes = socket.EndSend(ar);
        }, state);
    }

    private void Receive()
    {
        socket.BeginReceiveFrom(state.buffer, 0, bufferSize, SocketFlags.None, ref endPoint, receive = (ar) =>
        {
            Status so = (Status)ar.AsyncState;
            int bytes = socket.EndReceiveFrom(ar, ref endPoint);
            IPEndPoint ipenPoint = endPoint as IPEndPoint;
            onReceived.Invoke(new Packet(ipenPoint.Address.ToString(), Encoding.ASCII.GetString(so.buffer, 0, bytes)));
            Receive();
        }, state);
    }

    public static string GetLocalIPAddress()
    {
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        return localIP;
    }

}