using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region ENUM
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

    public enum EWaveType
    {
        None,
        RedZone,
        Elete,
        Boss
    }

    public enum ECreatureState
    {
        Idle,
        Skill,
        Moving,
        OnDamaged,
        Dead
    }

    public enum EObjectType
    {
        Player,
        Monster,
        EliteMonster,
        Boss,
        Projectile,
        Gem,
        Soul,
        Potion,
        DropBox,
        Magnet,
        Bomb
    }

    public enum ESkillType
    {
        None,
        Melee,
        Projectile,
        Misc,
    }

    public enum EJoystickType
    {
        Fixed,
        Flexible
    }

    public enum EUIEvent
    {
        Click,
        Preseed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }
#endregion

    public const int PLAYER_DATA_ID = 1;
    public const string EXP_GEM_PREFAB = "ExpGem.prefab";

    public const int SWORD_ID = 10;
}
