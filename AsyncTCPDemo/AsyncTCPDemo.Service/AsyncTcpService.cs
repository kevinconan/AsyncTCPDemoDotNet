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

namespace AsyncTCPDemo.Service
{
    public class AsyncTcpService
    {
        private IPAddress ipAddress;
        private int port;

        public AsyncTcpService(int port)
        {
            this.port = port;
            ipAddress = IPAddress.Any;
        }

        public async void Run()
        {
            TcpListener listener = new TcpListener(this.ipAddress, this.port);
            listener.Start();
            Console.WriteLine("Service started.");

            for (; ; )
            {
                try
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    await Process(tcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// 处理异步请求
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        private async Task Process(TcpClient tcpClient)
        {
            string clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Received connection request from " + clientEndPoint);

            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);

                writer.AutoFlush = true;
                while (true)
                {
                    string base64 = await reader.ReadLineAsync(); //异步读取
                    Message msg = ModelSerializationHelper.Deserialize<Message>(base64);

                    if (msg != null)
                    {
                        People p = msg.Content as People;

                        if (p != null)
                        {
                            Console.WriteLine(p.SayHello() + "  ----------  " + p.HowOldAreYou());
                        }
                        else
                        {
                            Console.WriteLine("Received an unkonwn message from {0}! ", clientEndPoint);
                        }

                        string response = ModelSerializationHelper.Serialize(new Message { Content = Response(p) });

                        Console.WriteLine("Computed response is: " + response + "\n");
                        await writer.WriteLineAsync(response);
                    }
                    else
                        break; // Client closed connection
                }
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                }
            }
        }

        private static string Response(People p)
        {
            if (p != null)
            {
                return string.Format("Hello {0}!", p.Name);
            }
            else
            {
                return string.Format("There is something wrong with your message!");
            }
        }

    }
}
