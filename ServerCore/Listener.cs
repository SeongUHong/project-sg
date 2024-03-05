using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    public class Listener
    {
        Socket _listenSocket;
        Func<SessionBase> _sessionFactory;

        public void Init(IPEndPoint endPoint, Func<SessionBase> sessionFactory, int register = 10, int backlog = 100)
        {
            // 소켓 초기화
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // 연결에 성공했을 때의 처리 초기화
            _sessionFactory += sessionFactory;

            // 연결
            _listenSocket.Bind(endPoint);
            // 소켓을 수신 상태로 두고 최대 대기 수 설정
            _listenSocket.Listen(backlog);

            // 비동기 작업 수행 (다수 실행)
            for (int i = 0; i < register; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);
            }

        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            // 소켓을 설정한 상태로 대기할 경우 에러가 발생하기 때문에 초기화해줌
            args.AcceptSocket = null;

            bool pending = _listenSocket.AcceptAsync(args);
            if (pending == false)
                OnAcceptCompleted(null, args);
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // 지정된 세션 생성
                SessionBase session = _sessionFactory.Invoke();
                // 리스너가 받아온 소켓을 세션에게 전달
                // 이후의 통신은 소켓을 전달받은 세션이 수행함
                session.Start(args.AcceptSocket);
                session.OnConnected(args.AcceptSocket.RemoteEndPoint);
            }
            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }

            // 연결 완료 처리가 끝나면 다시 대기 상태로
            RegisterAccept(args);
        }
    }
}
