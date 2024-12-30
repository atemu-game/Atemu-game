using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine;

namespace TcgEngine
{
    //Just a wrapper of UnityTransport to make it easier to replace with WebSocketTransport if planning to build for WebGL

    public class TcgTransport : MonoBehaviour
    {
        // [Header("Client")]
        // [TextArea] public string chain;

        // [Header("Server")]
        // [TextArea] public string cert;
        // [TextArea] public string key;         //Set this on server only

        private UnityTransport transport;

        private const string listen_all = "0.0.0.0";

        public virtual void Init()
        {
            transport = GetComponent<UnityTransport>();
        }

        public virtual void SetServer(ushort port)
        {
            transport.ConnectionData.ServerListenAddress = listen_all;
            transport.SetConnectionData(listen_all, port);
            // transport.SetServerSecrets(cert, key);

        }

        public virtual void SetClient(string address, ushort port)
        {
            // string ip = NetworkTool.HostToIP(address);
            string ip = "127.0.0.1";

#if !UNITY_WEBGL || UNITY_EDITOR
            System.Net.IPAddress[] addresses = System.Net.Dns.GetHostAddresses(address);
            ip = addresses[0].ToString();
#endif
            Debug.Log(ip + " " + address);
            transport.SetConnectionData(ip, port);
            transport.SetClientSecrets(address);
            Debug.Log("SetClientSecrets " + address);

        }

        public virtual string GetAddress() { return transport.ConnectionData.Address; }
        public virtual ushort GetPort() { return transport.ConnectionData.Port; }
    }
}
