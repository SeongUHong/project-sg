using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Fireball
    {
        public ushort FireballId { get; set; }
        public ushort PlayerId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float RotZ { get; set; }
    }
}
