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
            IPAddress ipAddr = IPAddress.Parse(Config.SERVER_IP);
            IPEndPoint endPoint = new IPEndPoint(ipAddr, Config.SERVER_PORT);

            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Server Start Operation.");

            while (true)
            {
                JobTimer.Instance.Flush();
            }
        }
    }
}
