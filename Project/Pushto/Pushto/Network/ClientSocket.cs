using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Pushto.Network
{
    abstract class ClientSocket
    {
        private TcpClient client;
        private bool connected = false;
        public StreamWriter writer;
        private readonly string HOST = "localhost";
        public ClientSocket()
        {
            new Thread(new ThreadStart(Connect)).Start();
        }

        private void Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect("localhost", 1992);

                using (StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII))
                {
                    writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
                    connected = true;
                    OnConnected();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        OnData(JObject.Parse(line));
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Socket", "Thread crashed: " + e.ToString());
                client.Close();
                connected = false;
                OnDisconnected();
                Logger.Verbose("Socket", "Waiting before trying reconnect.");
                Util.RunLater(() => { Connect(); }, "Reconnect", TimeSpan.FromSeconds(30));
            }
        }

        public abstract void OnData(JObject data);

        public abstract void OnConnected();

        public abstract void OnDisconnected();

        public void SendData(JObject data)
        {
            if (connected)
            {
                data.ToString(Newtonsoft.Json.Formatting.None);
            }
        }
    }
}
