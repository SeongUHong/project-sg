using System;
using System.Collections.Generic;
using System.Text;

namespace DummyClient
{
    class SessionManager
    {
        static SessionManager _sessionManager = new SessionManager();
        public static SessionManager Instance { get { return _sessionManager; } }

        List<ServerSession> _sessions = new List<ServerSession>();
        int SessionId = 1;
        object _lock = new object();
        Random _rand = new Random();

        public ServerSession Generate()
        {
            lock (_lock)
            {
                ServerSession session = new ServerSession();
                session.Id = SessionId++;
                _sessions.Add(session);
                return session;
            }
        }

        public void SendForEach()
        {
            lock (_lock)
            {
                string testName = "testName";
                foreach (ServerSession session in _sessions)
                {
                    if (session.IsMatched)
                        continue;

                    C_StartMatch startMatch = new C_StartMatch();
                    startMatch.nickname = testName + session.Id;
                    session.Send(startMatch.Write());
                }
            }
        }

        public void HandleMatched(ServerSession session, S_Matched matched)
        {
            lock (_lock)
            {
                session.IsMatched = true;
                Console.WriteLine($"Matched With : {matched.enemyNickname}");
            }
        }
    }
}
