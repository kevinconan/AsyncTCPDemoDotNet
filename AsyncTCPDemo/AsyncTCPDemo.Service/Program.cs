using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTCPDemo.Common;

namespace AsyncTCPDemo.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AsyncTcpService service = new AsyncTcpService(ServerConfigs.PORT);
                service.Run();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
