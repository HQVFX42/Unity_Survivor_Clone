using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region ENUM

    public enum EMaterialType
    {
        Gold,
        Dia,
        Stamina,
        Exp,
        WeaponScroll,
        GlovesScroll,
        RingScroll,
        BeltScroll,
        ArmorScroll,
        BootsScroll,
        BronzeKey,
        SilverKey,
        GoldKey,
        RandomScroll,
        AllRandomEquipmentBox,
        RandomEquipmentBox,
        CommonEquipmentBox,
        UncommonEquipmentBox,
        RareEquipmentBox,
        EpicEquipmentBox,
        LegendaryEquipmentBox,
        WeaponEnchantStone,
        GlovesEnchantStone,
        RingEnchantStone,
        BeltEnchantStone,
        ArmorEnchantStone,
        BootsEnchantStone,
    }
    public enum MaterialGrade
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Epic1,
        Epic2,
        Legendary,
        Legendary1,
        Legendary2,
        Legendary3,
    }

    public enum EScene
    {
        Unknown,
        TitleScene,
        LobbyScene,
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

    public enum ECreatureType
    {
        None,
        Player,
        Monster,
        RegularMonster,
        Boss
    }

    public enum ECreatureState
    {
        Idle,
        Moving,
        Skill,
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
        Sword,
        Sequence,
        Repeat,
        Dash,
        Melee,
        Projectile,
        Misc,
    }

    public enum ESupportSkillName
    {
        Critical,
        MaxHpBonus,
        ExpBonus,
        SoulBonus,
        DamageReduction,
        AtkBonusRate,
        MoveBonusRate,
        Healing, // ü�� ȸ�� 
        HealBonusRate,//ȸ���� ����
        HpRegen,
        CriticalDamage,
        MagneticRange,
        Resurrection,
        LevelupMoveSpeed,
        LevelupReduction,
        LevelupAtk,
        LevelupCri,
        LevelupCriDmg,
        MonsterKillAtk,
        MonsterKillMaxHP,
        MonsterKillReduction,
        EliteKillExp,
        EliteKillSoul,
        EnergyBolt,
        IcicleArrow,
        PoisonField,
        EletronicField,
        Meteor,
        FrozenHeart,
        WindCutter,
        EgoSword,
        ChainLightning,
        Shuriken,
        ArrowShot,
        SavageSmash,
        PhotonStrike,
        StormBlade,
    }

    public enum ESupportSkillGrade
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legend
    }

    public enum ESupportSkillType
    {
        General,
        Passive,
        LevelUp,
        MonsterKill,
        EliteKill,
        Special
    }

    public enum EMissionType
    {
        Complete, // �Ϸ��
        Daily,
        Weekly,
    }

    public enum EMissionTarget // �̼� ����
    {
        DailyComplete, // ���ϸ� �Ϸ�
        WeeklyComplete, // ��Ŭ�� �Ϸ�
        StageEnter, // �������� ����
        StageClear, // �������� Ŭ����
        EquipmentLevelUp, // ��� ������
        CommonGachaOpen, // �Ϲ� ��í ���� (���� ��������)
        AdvancedGachaOpen, // ��� ��í ���� (���� ��������)
        OfflineRewardGet, // �������� ���� 
        FastOfflineRewardGet, // ���� �������� ����
        ShopProductBuy, // ���� ��ǰ ����
        Login, // �α���
        EquipmentMerge, // ��� �ռ�
        MonsterAttack, // ���� ����
        MonsterKill, // ���� ų
        EliteMonsterAttack, // ����Ʈ ����
        EliteMonsterKill, // ����Ʈ ų
        BossKill, // ���� ų
        DailyShopBuy, // ���ϸ� ���� ��ǰ ����
        GachaOpen, // ��í ���� (�Ϲ�, ��ް�í ����)
        ADWatchIng, // ���� ��û
    }

    public enum EGachaRarity
    {
        Normal,
        Special,
    }

    public enum EEquipmentType
    {
        Weapon,
        Gloves,
        Ring,
        Belt,
        Armor,
        Boots,
    }
    public enum EEquipmentGrade
    {
        None,
        Common,
        Uncommon,
        Rare,
        Epic,
        Epic1,
        Epic2,
        Legendary,
        Legendary1,
        Legendary2,
        Legendary3,
        Myth,
        Myth1,
        Myth2,
        Myth3
    }

    public enum EEquipmentSortType
    {
        Level,
        Grade,
    }

    public enum EMergeEquipmentType
    {
        None,
        ItemCode,
        Grade,
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

    public static readonly Dictionary<Type, Array> _enumDict = new Dictionary<Type, Array>();

    public const int PLAYER_DATA_ID = 1;
    public const string EXP_GEM_PREFAB = "ExpGem.prefab";

    public const int SWORD_ID = 10;

    public static int MAX_STAMINA = 50;

    #region ���� ����ġ ȹ�淮
    public const int SMALL_EXP_AMOUNT = 1;
    public const int GREEN_EXP_AMOUNT = 2;
    public const int BLUE_EXP_AMOUNT = 5;
    public const int YELLOW_EXP_AMOUNT = 10;
    #endregion

    #region ����Ʈ ���/�ɸ��� ���̵�
    public const int CHARACTER_DEFAULT_ID = 201000;
    public const string WEAPON_DEFAULT_ID = "N00301";
    public const string GLOVES_DEFAULT_ID = "N10101";
    public const string RING_DEFAULT_ID = "N20201";
    public const string BELT_DEFAULT_ID = "N30101";
    public const string ARMOR_DEFAULT_ID = "N40101";
    public const string BOOTS_DEFAULT_ID = "N50101";
    #endregion

    #region �����;��̵�
    public static int ID_GOLD = 50001;
    public static int ID_DIA = 50002;
    public static int ID_STAMINA = 50003;
    public static int ID_BRONZE_KEY = 50201;
    public static int ID_SILVER_KEY = 50202;
    public static int ID_GOLD_KEY = 50203;
    public static int ID_RANDOM_SCROLL = 50301;
    public static int ID_POTION = 60001;
    public static int ID_MAGNET = 60004;
    public static int ID_BOMB = 60008;

    public static int ID_WEAPON_SCROLL = 50101;
    public static int ID_GLOVES_SCROLL = 50102;
    public static int ID_RING_SCROLL = 50103;
    public static int ID_BELT_SCROLL = 50104;
    public static int ID_ARMOR_SCROLL = 50105;
    public static int ID_BOOTS_SCROLL = 50106;

    public static string GOLD_SPRITE_NAME = "Gold_Icon";
    #endregion
}

public static class EquipmentUIColors
{
    #region ��� �̸� ����
    public static readonly Color CommonNameColor = Utils.HexToColor("A2A2A2");
    public static readonly Color UncommonNameColor = Utils.HexToColor("57FF0B");
    public static readonly Color RareNameColor = Utils.HexToColor("2471E0");
    public static readonly Color EpicNameColor = Utils.HexToColor("9F37F2");
    public static readonly Color LegendaryNameColor = Utils.HexToColor("F67B09");
    public static readonly Color MythNameColor = Utils.HexToColor("F1331A");
    #endregion
    #region �׵θ� ����
    public static readonly Color Common = Utils.HexToColor("AC9B83");
    public static readonly Color Uncommon = Utils.HexToColor("73EC4E");
    public static readonly Color Rare = Utils.HexToColor("0F84FF");
    public static readonly Color Epic = Utils.HexToColor("B740EA");
    public static readonly Color Legendary = Utils.HexToColor("F19B02");
    public static readonly Color Myth = Utils.HexToColor("FC2302");
    #endregion
    #region ������
    public static readonly Color EpicBg = Utils.HexToColor("D094FF");
    public static readonly Color LegendaryBg = Utils.HexToColor("F8BE56");
    public static readonly Color MythBg = Utils.HexToColor("FF7F6E");
    #endregion
}