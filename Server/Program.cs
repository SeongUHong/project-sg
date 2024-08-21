using ServerCore;
using System;
using System.Net;

namespace Server
{
    class Program
    {
        static Listener _listener = new Listener();

        static void Main(string[] args)
        {
            // Local
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            // surviveinjapan.com
            //IPAddress ipAddr = IPAddress.Parse(Config.SERVER_IP);
            //IPEndPoint endPoint = new IPEndPoint(ipAddr, Config.SERVER_PORT);

            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Server Start Operation.");

            while (true)
            {
                JobTimer.Instance.Flush();
            }
        }
    }
}
