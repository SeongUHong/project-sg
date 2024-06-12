using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Player
    {
        public ushort PlayerId { get; set; }
        public ushort EnemyPlayerId { get; set; }
        public string Nickname { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float RotZ { get; set; }
        public bool IsReady { get; set; }
        public ushort HitCount { get; set; }
    }
}
