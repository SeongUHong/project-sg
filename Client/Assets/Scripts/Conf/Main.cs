using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Conf
{
    public class Main
    {
        public const string SERVER_IP = "108.61.223.19";
        public const int SERVER_PORT = 7777;

        public static float UNIT_SPAWN_RANGE = 1.0f;
        public static float MONSTER_SPAWN_RANGE = 3.0f;
        public static InGame_NickName_Panel _inGameNick;
        public static List<String> PLAYER_ID_LIST = new List<String>();
        public static List<String> ENEMY_ID_LIST = new List<String>();
        public enum ATTACK_FLAG
        {
            ATTACK = 1,
        }
        public enum GAMEOVER_STATUS
        {
            WIN,
            LOSE,
            DRAW,
        }
        public static float SEND_PACKET_INTERVAL = 0.25f;
        public static float MOVE_POWER = 1.0f;

    }

}