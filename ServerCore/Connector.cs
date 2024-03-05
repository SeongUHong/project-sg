using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    public class Connector
    {
        Func<SessionBase> _sessionFactory;

        public void Connect(IPEndPoint endPoint, Func<SessionBase> sessionFactory, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                // 소켓 생성
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // 연결 성공했을 때 세션을 반환해줄 오브젝트
                _sessionFactory = sessionFactory;

                // 비동기 소켓 작업 설정
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += OnConnectedComplete;
                args.RemoteEndPoint = endPoint;
                // 연결 소켓 설정 
                args.UserToken = socket;

                RegisterConnect(args);
            }
        }

        private void RegisterConnect(SocketAsyncEventArgs args)
        {
            // 비동기 작업과 연결된 소켓 취득
            Socket socket = args.UserToken as Socket;
            if (socket == null)
                return;

            bool pending = socket.ConnectAsync(args);
            if (pending == false)
                OnConnectedComplete(null, args);
        }

        private void OnConnectedComplete(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                SessionBase session = _sessionFactory.Invoke();
                // 연결이 완료된 소켓을 설정 후 세션 시작
                session.Start(args.ConnectSocket);
                session.OnConnected(args.RemoteEndPoint);
            }
            else
            {
                Console.WriteLine($"OnConnectedComplete Failed : {args.SocketError}");
            }
        }
    }
}
