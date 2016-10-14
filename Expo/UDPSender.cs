using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class UDPSender
{
    UdpClient udpClient;
    public int Port { get; private set; }
    

    public UDPSender(int port)
    {
        this.Port = port;
    }

    public bool SendMessage(Message message)
    {
        using (UdpClient uc = new UdpClient()) 
        {
            byte[] dataToSend = message.GetBytes();
            string ipAdress = GameObject.Find("ScriptObject").GetComponent<UnityTCPClient>().ipEndpoint;

            uc.Send(dataToSend, dataToSend.Length, ipAdress, Port);
            return true;
        }
    }
	
}
