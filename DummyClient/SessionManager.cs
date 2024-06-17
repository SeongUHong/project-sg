using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DummyClient
{
    class SessionManager
    {
        static SessionManager _sessionManager = new SessionManager();
        public static SessionManager Instance { get { return _sessionManager; } }

        List<ServerSession> _sessions = new List<ServerSession>();
        int SessionId = 1;
        object _lock = new object();
        int _time = 0;

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
                foreach (ServerSession session in _sessions)
                {
                    if (_time >= 20)
                        session.Send(new C_Destroyed().Write());

                    if (!session.IsMatched)
                    {
                        RequestMatch(session);
                    }
                    else if (!session.IsReady) {
                        Ready(session);
                    }
                    else
                    {
                        if (session.ShootCount <= 3)
                        {
                            Shot(session);
                        }
                        else
                        {
                            Move(session);
                        }
                    }

                    if (_time >= 15 && !session.IsHit)
                    {
                        foreach (ServerSession s in _sessions)
                        {
                            if (session != s)
                                Hit(session, s);
                        }
                    }
                }
            }
        }

        public void RequestMatch(ServerSession session)
        {
            string testName = "testName";
            C_StartMatch startMatch = new C_StartMatch();
            startMatch.nickname = testName + session.Id;
            session.Send(startMatch.Write());
        }

        public void Ready(ServerSession session)
        {
            session.Send(new C_ReadyBattle().Write());
        }

        public void Shot(ServerSession session)
        {
            Random rand = new Random();

            C_Shot shot = new C_Shot()
            {
                posX = rand.Next(1, 10),
                posY = rand.Next(1, 10),
                angle = (float)rand.Next(0, 100) / 100f,
            };
            session.Send(shot.Write());

            session.ShootCount++;
        }

        public void Move(ServerSession session)
        {
            Random rand = new Random();

            C_Move move = new C_Move()
            {
                posX = rand.Next(1, 10),
                posY = rand.Next(1, 10),
                angle = (float)rand.Next(0, 100) / 100f,
            };
            session.Send(move.Write());
        }

        public void Hit(ServerSession session, ServerSession anotherSession)
        {
            int fireballId = anotherSession.FirstFireballId;
            if (fireballId < 0)
            {
                Console.WriteLine($"Cant find fireballId ({fireballId})");
                return;
            }

            session.Send(new C_Hit()
            {
                fireballId = fireballId
            }.Write());
        }

        public void ElapseTime(object sender, ElapsedEventArgs e)
        {
            _time += 1;
            Console.WriteLine($"timer : {_time}");
        }
    }
}
