using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncTCPDemo.Models;

namespace AsyncTCPDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Send message " + i);

                new ThreadStart(delegate()
                {
                    try
                    {
                        AsyncTcpClient host = new AsyncTcpClient(IPAddress.Loopback);
                        Message m = new Message { Content = new People { Name = "P" + i, Age = rnd.Next(i * 10) } };
                        Send(host, m);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }).Invoke();
            }

            Console.ReadLine();
        }

        private static async void Send(AsyncTcpClient host, Message m)
        {
            string s = await host.SendToServer(m);
            Console.WriteLine(s);
        }


    }
}
