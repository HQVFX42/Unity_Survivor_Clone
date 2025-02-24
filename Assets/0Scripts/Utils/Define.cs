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

    public enum ESkillType
    {
        None,
        Melee,
        Projectile,
        Misc,
    }

    public const int PLAYER_DATA_ID = 1;
    public const string EXP_GEM_PREFAB = "ExpGem.prefab";

    public const int SWORD_ID = 10;
}
