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
        public int RegTime { get; set; }

        public bool CanRemove(int time)
        {
            if (RegTime - Config.FIREBALL_LIMIT_TIME > time)
                return true;

            return false;
        }
    }
}
