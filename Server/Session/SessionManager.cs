using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class SessionManager
    {
        static SessionManager _sessionManager = new SessionManager();
        public static SessionManager Instance { get { return _sessionManager; } }

        int _sessionId = 0;
        Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
        object _lock = new object();

        // 세션 생성
        public ClientSession Generate()
        {
            lock (_lock)
            {
                int sessionId = ++_sessionId;
                ClientSession session = new ClientSession();
                session.SessionId = sessionId;
                // Dictionary에 세션 저장
                _sessions.Add(sessionId, session);

                return session;
            }
        }

        // 세션 찾기
        public ClientSession Find(int sessionId)
        {
            lock (_lock)
            {
                ClientSession session = null;
                _sessions.TryGetValue(sessionId, out session);
                return session;
            }
        }

        // 세션 삭제
        public void Remove(ClientSession session)
        {
            lock (_lock)
            {
                _sessions.Remove(session.SessionId);
            }
        }
    }
}
