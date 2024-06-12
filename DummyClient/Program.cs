using ServerCore;
using System;
using System.Net;
using System.Threading;
using System.Timers;

namespace DummyClient
{
    class Program
    {
        public static System.Timers.Timer timer = new System.Timers.Timer(1000);

        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();

            connector.Connect(endPoint,
                () => { return SessionManager.Instance.Generate(); },
                2);

            timer.Elapsed += SessionManager.Instance.ElapseTime;
            timer.AutoReset = true;
            timer.Enabled = true;

            while (true)
            {
                try
                {
                    SessionManager.Instance.SendForEach();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Thread.Sleep(1000);
            }
        }
    }
}
