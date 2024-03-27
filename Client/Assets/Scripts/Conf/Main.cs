using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Conf
{
    public class Main
    {
        public static float UNIT_SPAWN_RANGE = 1.0f;
        public static float MONSTER_SPAWN_RANGE = 3.0f;
        public static int CURRENT_STAGE = 1;
        public static bool CLEAR_FLAG = false;
        public static int CURRENT_SCENE = 1;
        public static string SCENE_NAME = "GameSceneStage";
        public static string ChosenShip = null;
        public static bool PLAYER_DEAD_FLAG = false;
        public static bool ENEMY_DEAD_FLAG = false;
        public static Result_Panel _result;
        public static List<String> PLAYER_ID_LIST = new List<String>();
        public static List<String> ENEMY_ID_LIST = new List<String>();
        public enum ATTACK_FLAG
        {
            ATTACK = 1,
        }
        public static bool LOADING_FLAG = false;
    }

}