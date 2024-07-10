using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Config
    {
        // 배틀 중 패킷 전송 간격
        public const int FLUSH_BATTLE_JOB_INTERVAL = 250;
        // 배틀 플레이어 수
        public const int BATTLE_PLAYER_NUM = 2;
        // 게임 제한 시간
        public const int GAME_TIME_LIMIT = 180;
        // 게임 제한 시간 카운트 간격
        public const int GAME_TIME_COUNT_INTERVAL = 1000;
        // 미사일 생존 시간
        public const int FIREBALL_LIMIT_TIME = 10;
        // 미사일 삭제 간격
        public const int REMOVE_FIREBALL_JOB_INTERVAL = 1000;
        // 게임 종료후 세션 종료 까지의 텀
        public const int DISCONNECT_SESSION_DELAY = 2000;

        public enum GAMEOVER_STATUS
        {
            WIN,
            LOSE,
            DROW
        }
    }
}
