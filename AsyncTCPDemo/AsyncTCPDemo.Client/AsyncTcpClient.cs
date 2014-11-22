using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AsyncTCPDemo.Common;
using AsyncTCPDemo.Models;

namespace AsyncTCPDemo.Client
{
    public class AsyncTcpClient
    {
        private IPAddress host;

        public AsyncTcpClient(IPAddress host)
        {
            this.host = host;
        }
        public async Task<string> SendToServer(Message msg)
        {
            TcpClient server = new TcpClient();
            string base64 = ModelSerializationHelper.Serialize(msg);


            await server.ConnectAsync(host, ServerConfigs.PORT);

            NetworkStream ns = server.GetStream();
            StreamWriter writer = new StreamWriter(ns);
            StreamReader reader = new StreamReader(ns);

            writer.AutoFlush = true;

            await writer.WriteLineAsync(base64);

            Console.WriteLine("--Message sent, awaiting for response.");

            string rec = await reader.ReadLineAsync();

            if (server.Connected)
            {
                server.Close();
            }

            Message m = ModelSerializationHelper.Deserialize<Message>(rec);
            return string.Format("{0} ---- Sent at:{1}", m.Content, m.CreateTime);


        }
    }
}
