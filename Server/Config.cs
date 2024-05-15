using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Config
    {
        // 배틀 중 패킷 전송 간격
        public const int FLUSH_BATTLE_JOB_INTERVAL = 250;
        // 미사일 이동 패킷 전송 간격
        public const int MOVE_FIREBALL_JOB_INTERVAL = 500;

        public const int BATTLE_PLAYER_NUM = 2;
        // 미사일 이동 속도
        public const float FIREBALL_SPEED = 1.0f;
        // 미사일 이동 처리 간격
        public const float MOVE_FIREBALL_INTERVAL = 0.5f;
    }
}
