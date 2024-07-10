using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace data
{
    //엔티티 클래스
    [Serializable]
    public class Stat
    {
        //레벨(주키 역할)
        public int level;
        public int hp;
        //공격력
        public int offence;
        //방어력
        public int defence;
        //이동속도
        public float move_speed;
        //공격사거리
        public float attack_distance;
        //공격속도
        public float attack_speed;
        //투사체 속도
        public float projectile_speed;
        //어택 게이지
        public float attack_gauge;
        //맥스 어택게이지
        public float max_attack_gauge;
    }

    [Serializable]
    public class StatLoader : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
            {
                dict.Add(stat.level, stat);
            }

            return dict;
        }
    }
}
