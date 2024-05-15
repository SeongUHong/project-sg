using System;
using System.Numerics;

namespace Server
{
    class Fireball
    {
        public ushort FireballId { get; set; }
        public ushort PlayerId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float RotZ { get; set; }

        public void Move()
        {
            PosX += (float)Math.Cos(RotZ) * Config.FIREBALL_SPEED * Config.MOVE_FIREBALL_INTERVAL;
            PosY += (float)Math.Sin(RotZ) * Config.FIREBALL_SPEED * Config.MOVE_FIREBALL_INTERVAL;
        }
    }
}
