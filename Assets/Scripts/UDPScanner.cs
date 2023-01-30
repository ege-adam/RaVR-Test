using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class UDPScanner : MonoBehaviour
{
    private UDPSocket serverUDP;
    Thread serverThread;

    private void Start() {
        serverUDP = new UDPSocket();
        serverThread = new Thread(() => { serverUDP.Server(UDPSocket.GetLocalIPAddress(), 3994, OnServerReceive); });


        Debug.Log(serverThread.ThreadState);

        Invoke(nameof(ScanTrackers), 4f);
    }

    private void Update() {
        if(serverThread.ThreadState != ThreadState.Running) 
        serverThread.Start();

        
    }

    public void OnServerReceive(Packet packet)
    {
        Debug.Log(serverThread.ThreadState);

        Debug.Log("RecFrom: " + packet.SenderIP);
        switch (packet.Data)
        {
            case string s when s.StartsWith("iameyeofra"):
                Debug.Log("Found eye on: " + packet.SenderIP);
                break;
            case string s when s.StartsWith("dt"):
                //Debug.Log(s.Substring(2));
                Debug.Log(packet.Data);

                break;
            default:
                Debug.Log(packet.Data);
                break;
        }
    }

    public void ScanTrackers()
    {

        string ipMask;

        ipMask = "192.168.31.";

        UDPSocket scannerClient = new UDPSocket();
        scannerClient.Client(ipMask + "139", 3994);
        scannerClient.Send("areyoumyeye");

        /*
        for (int i = 0; i < 256; i++)
        {

            scannerClient.Client(ipMask + i.ToString(), 3994);
            scannerClient.Send("areyoumyeye");
           // Debug.Log(i);
        } */

        Debug.Log(serverThread.ThreadState);

    }
}
