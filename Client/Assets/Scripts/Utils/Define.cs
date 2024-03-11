using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scenes
    {
        Unknown,
        MainScene,
        SelectScene,
        GameScene,

    }

    public enum State
    {
        Die,
        Moving,
        Idle,
        Attack,
        Skill,
        Clear,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Press,
        PointerDown,
        PointerUp,
    }

    public enum SceneLocateObject
    {
        PlayerSpawnSpot,
        EnemySpawnSpot,
    }

    public enum Layer
    {
        Player = 7,
        Enemy = 8,
        Player_Bullet = 9,
        Enemy_Buleet = 10,
    }

    public enum Skill
    {
        Launch,
        Burf,
        fireballredbig,
        fireballbluebig,
        Missile,
    }

    public const float DESPAWN_DELAY_TIME = 1.0f;
    public const float RETRY_DELAY_TIME = 2.0f;
    public const float NEXT_DELAY_TIME = 1.0f;
}
