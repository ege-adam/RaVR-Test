using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Packet
{
    private string senderIP;
    private string data;

    public string SenderIP { get => senderIP; }
    public string Data { get => data; }

    public Packet(string sender, string data)
    {
        this.senderIP = sender;
        this.data = data;
    }

}
