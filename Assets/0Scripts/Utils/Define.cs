using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum EScene
    {
        Unknown,
        DevScene,
        GameScene,
    }

    public enum ESound
    {
        Bgm,
        Effect,
    }

    public enum EObjectType
    {
        Player,
        Monster,
        Projectile,
        Env,
    }
}
