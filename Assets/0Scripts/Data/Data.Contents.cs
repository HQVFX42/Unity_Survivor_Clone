using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Data
{
    #region Xml

    //#region PlayerData

    //public class PlayerData
    //{
    //    [XmlAttribute]
    //    public int level;
    //    [XmlAttribute]
    //    public int maxHp;
    //    [XmlAttribute]
    //    public int attack;
    //    [XmlAttribute]
    //    public int totalExp;
    //}

    //[Serializable, XmlRoot("PlayerDataSet")]
    //public class PlayerDataLoader : ILoader<int, PlayerData>
    //{
    //    [XmlElement("PlayerData")]
    //    public List<PlayerData> stats = new List<PlayerData>();

    //    public Dictionary<int, PlayerData> MakeDictionary()
    //    {
    //        Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
    //        foreach (PlayerData stat in stats)
    //        {
    //            dict.Add(stat.level, stat);
    //        }
    //        return dict;
    //    }
    //}
    //#endregion

    //#region MonsterData

    //public class MonsterData
    //{
    //    [XmlAttribute]
    //    public int name;
    //    [XmlAttribute]
    //    public string prefab;
    //    [XmlAttribute]
    //    public int level;
    //    [XmlAttribute]
    //    public int maxHp;
    //    [XmlAttribute]
    //    public int attack;
    //    [XmlAttribute]
    //    public int speed;
    //}

    //#endregion

    //#region SkillData

    //[Serializable]
    //public class HitEffect
    //{
    //    [XmlAttribute]
    //    public string type;
    //    [XmlAttribute]
    //    public int templateID;
    //    [XmlAttribute]
    //    public int value;
    //}

    //public class SkillData
    //{
    //    [XmlAttribute]
    //    public int templateID;

    //    [XmlAttribute(AttributeName = "type")]
    //    //public string skillTypeStr;
    //    public Define.ESkillType skillType = Define.ESkillType.None;

    //    [XmlAttribute]
    //    public int nextID;
    //    public int prevID = 0;

    //    [XmlAttribute]
    //    public string prefab;

    //    [XmlAttribute]
    //    public int damage;

    //    //[XmlElement("HitEffect")]
    //    //public List<HitEffect> hitEffects = new List<HitEffect>();
    //}

    //[Serializable, XmlRoot("SkillDatas")]
    //public class SkillDataLoader : ILoader<int, SkillData>
    //{
    //    [XmlElement("SkillData")]
    //    public List<SkillData> skills = new List<SkillData>();

    //    public Dictionary<int, SkillData> MakeDictionary()
    //    {
    //        Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
    //        foreach (SkillData skill in skills)
    //            dict.Add(skill.templateID, skill);

    //        return dict;
    //    }
    //}

    //#endregion

    #endregion

    #region Json

    #region CreatureData
    [Serializable]
    public class CreatureData
    {
        public int DataId;
        public string DescriptionTextID;
        public string PrefabLabel;
        public float MaxHp;
        public float MaxHpBonus;
        public float Atk;
        public float AtkBonus;
        public float Def;
        public float MoveSpeed;
        public float TotalExp;
        public float HpRate;
        public float AtkRate;
        public float DefRate;
        public float MoveSpeedRate;
        public string IconLabel;
        public List<int> SkillTypeList;//InGameSkills를 제외한 추가스킬들
    }

    [Serializable]
    public class CreatureDataLoader : ILoader<int, CreatureData>
    {
        public List<CreatureData> creatures = new List<CreatureData>();
        public Dictionary<int, CreatureData> MakeDictionary()
        {
            Dictionary<int, CreatureData> dict = new Dictionary<int, CreatureData>();
            foreach (CreatureData creature in creatures)
                dict.Add(creature.DataId, creature);
            return dict;
        }
    }
    #endregion


    #region WaveData
    [System.Serializable]
    public class WaveData
    {
        public int StageIndex = 1;
        public int WaveIndex = 1;
        public float SpawnInterval = 0.5f;
        public int OnceSpawnCount;
        public List<int> MonsterId;
        public List<int> EleteId;
        public List<int> BossId;
        public float RemainsTime;
        public Define.EWaveType WaveType;
        public float FirstMonsterSpawnRate;
        public float HpIncreaseRate;
        public float nonDropRate;
        public float SmallGemDropRate;
        public float GreenGemDropRate;
        public float BlueGemDropRate;
        public float YellowGemDropRate;
        public List<int> EliteDropItemId;
    }

    public class WaveDataLoader : ILoader<int, WaveData>
    {
        public List<WaveData> waves = new List<WaveData>();

        public Dictionary<int, WaveData> MakeDictionary()
        {
            Dictionary<int, WaveData> dict = new Dictionary<int, WaveData>();
            foreach (WaveData wave in waves)
                dict.Add(wave.WaveIndex, wave);
            return dict;
        }
    }
    #endregion

    #region SkillData
    [Serializable]
    public class SkillData
    {
        public int DataId;
        public string Name;
        public string Description;
        public string PrefabLabel; //프리팹 경로
        public string IconLabel;//아이콘 경로
        public string SoundLabel;// 발동사운드 경로
        public string Category;//스킬 카테고리
        public float CoolTime; // 쿨타임
        public float DamageMultiplier; //스킬데미지 (곱하기)
        public float ProjectileSpacing;// 발사체 사이 간격
        public float Duration; //스킬 지속시간
        public float RecognitionRange;//인식범위
        public int NumProjectiles;// 회당 공격횟수
        public string CastingSound; // 시전사운드
        public float AngleBetweenProj;// 발사체 사이 각도
        public float AttackInterval; //공격간격
        public int NumBounce;//바운스 횟수
        public float BounceSpeed;// 바운스 속도
        public float BounceDist;//바운스 거리
        public int NumPenerations; //관통 횟수
        public int CastingEffect; // 스킬 발동시 효과
        public string HitSoundLabel; // 히트사운드
        public float ProbCastingEffect; // 스킬 발동 효과 확률
        public int HitEffect;// 적중시 이펙트
        public float ProbHitEffect; // 스킬 발동 효과 확률
        public float ProjRange; //투사체 사거리
        public float MinCoverage; //최소 효과 적용 범위
        public float MaxCoverage; // 최대 효과 적용 범위
        public float RoatateSpeed; // 회전 속도
        public float ProjSpeed; //발사체 속도
        public float ScaleMultiplier;
    }
    [Serializable]
    public class SkillDataLoader : ILoader<int, SkillData>
    {
        public List<SkillData> skills = new List<SkillData>();

        public Dictionary<int, SkillData> MakeDictionary()
        {
            Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
            foreach (SkillData skill in skills)
                dict.Add(skill.DataId, skill);
            return dict;
        }
    }
    #endregion

    #region SupportSkilllData
    [Serializable]
    public class SupportSkillData
    {
        public int AcquiredLevel;
        public int DataId;
        public Define.ESupportSkillType SupportSkillType;
        public Define.ESupportSkillName SupportSkillName;
        public Define.ESupportSkillGrade SupportSkillGrade;
        public string Name;
        public string Description;
        public string IconLabel;
        public float HpRegen;
        public float HealRate; // 회복량 (최대HP%)
        public float HealBonusRate; // 회복량 증가
        public float MagneticRange; // 아이템 습득 범위
        public int SoulAmount; // 영혼획득
        public float HpRate;
        public float AtkRate;
        public float DefRate;
        public float MoveSpeedRate;
        public float CriRate;
        public float CriDmg;
        public float DamageReduction;
        public float ExpBonusRate;
        public float SoulBonusRate;
        public float ProjectileSpacing;// 발사체 사이 간격
        public float Duration; //스킬 지속시간
        public int NumProjectiles;// 회당 공격횟수
        public float AttackInterval; //공격간격
        public int NumBounce;//바운스 횟수
        public int NumPenerations; //관통 횟수
        public float ProjRange; //투사체 사거리
        public float RoatateSpeed; // 회전 속도
        public float ScaleMultiplier;
        public float Price;
        public bool IsLocked = false;
        public bool IsPurchased = false;

        public bool CheckRecommendationCondition()
        {
            if (IsLocked == true || Managers.Game.SoulShopList.Contains(this) == true)
            {
                return false;
            }

            if (SupportSkillType == Define.ESupportSkillType.Special)
            {
                //내가 가지고 있는 장비스킬이 아니면 false
                if (Managers.Game.EquippedEquipments.TryGetValue(Define.EEquipmentType.Weapon, out Equipment myWeapon))
                {
                    int skillId = myWeapon.EquipmentData.BasicSkill;
                    Define.ESkillType type = Utils.GetSkillTypeFromInt(skillId);

                    switch (SupportSkillName)
                    {
                        case Define.ESupportSkillName.ArrowShot:
                        case Define.ESupportSkillName.SavageSmash:
                        case Define.ESupportSkillName.PhotonStrike:
                        case Define.ESupportSkillName.Shuriken:
                        case Define.ESupportSkillName.EgoSword:
                            if (SupportSkillName.ToString() != type.ToString())
                            {
                                return false;
                            }
                            break;
                    }

                }
            }
            #region 서포트 스킬 중복 방지 모드 보류
            //if (Managers.Game.Player.Skills.SupportSkills.TryGetValue(SupportSkillName, out var existingSkill))
            //{
            //    if (existingSkill == null)
            //        return true;

            //    if (DataId <= existingSkill.DataId)
            //    {
            //        return false;
            //    }
            //}
            #endregion

            return true;
        }
    }
    [Serializable]
    public class SupportSkillDataLoader : ILoader<int, SupportSkillData>
    {
        public List<SupportSkillData> supportSkills = new List<SupportSkillData>();

        public Dictionary<int, SupportSkillData> MakeDictionary()
        {
            Dictionary<int, SupportSkillData> dict = new Dictionary<int, SupportSkillData>();
            foreach (SupportSkillData skill in supportSkills)
                dict.Add(skill.DataId, skill);
            return dict;
        }
    }
    #endregion

    #region StageData
    [Serializable]
    public class StageData
    {
        public int StageIndex = 1;
        public string StageName;
        public int StageLevel = 1;
        public string MapName;
        public int StageSkill;

        public int FirstWaveCountValue;
        public int FirstWaveClearRewardItemId;
        public int FirstWaveClearRewardItemValue;

        public int SecondWaveCountValue;
        public int SecondWaveClearRewardItemId;
        public int SecondWaveClearRewardItemValue;

        public int ThirdWaveCountValue;
        public int ThirdWaveClearRewardItemId;
        public int ThirdWaveClearRewardItemValue;

        public int ClearReward_Gold;
        public int ClearReward_Exp;
        public string StageImage;
        public List<int> AppearingMonsters;
        public List<WaveData> WaveArray;
    }
    public class StageDataLoader : ILoader<int, StageData>
    {
        public List<StageData> stages = new List<StageData>();

        public Dictionary<int, StageData> MakeDictionary()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
            foreach (StageData stage in stages)
                dict.Add(stage.StageIndex, stage);
            return dict;
        }
    }
    #endregion

    #region EquipmentData
    [Serializable]
    public class EquipmentData
    {
        public string DataId;
        //public Define.GachaRarity GachaRarity;
        public Define.EEquipmentType EquipmentType;
        public Define.EEquipmentGrade EquipmentGrade;
        public string NameTextID;
        public string DescriptionTextID;
        public string SpriteName;
        public string HpRegen;
        public int MaxHpBonus;
        public int MaxHpBonusPerUpgrade;
        public int AtkDmgBonus;
        public int AtkDmgBonusPerUpgrade;
        public int MaxLevel;
        public int UncommonGradeSkill;
        public int RareGradeSkill;
        public int EpicGradeSkill;
        public int LegendaryGradeSkill;
        public int BasicSkill;
        //public Define.MergeEquipmentType MergeEquipmentType1;
        public string MergeEquipment1;
        //public Define.MergeEquipmentType MergeEquipmentType2;
        public string MergeEquipment2;
        public string MergedItemCode;
        public int LevelupMaterialID;
        public string DowngradeEquipmentCode;
        public string DowngradeMaterialCode;
        public int DowngradeMaterialCount;
    }

    [Serializable]
    public class EquipmentDataLoader : ILoader<string, EquipmentData>
    {
        public List<EquipmentData> Equipments = new List<EquipmentData>();
        public Dictionary<string, EquipmentData> MakeDictionary()
        {
            Dictionary<string, EquipmentData> dict = new Dictionary<string, EquipmentData>();
            foreach (EquipmentData equip in Equipments)
                dict.Add(equip.DataId, equip);
            return dict;
        }
    }
    #endregion

    #region MaterialtData
    [Serializable]
    public class MaterialData
    {
        public int DataId;
        public Define.EMaterialType MaterialType;
        public Define.MaterialGrade MaterialGrade;
        public string NameTextID;
        public string DescriptionTextID;
        public string SpriteName;

    }

    [Serializable]
    public class MaterialDataLoader : ILoader<int, MaterialData>
    {
        public List<MaterialData> Materials = new List<MaterialData>();
        public Dictionary<int, MaterialData> MakeDictionary()
        {
            Dictionary<int, MaterialData> dict = new Dictionary<int, MaterialData>();
            foreach (MaterialData mat in Materials)
                dict.Add(mat.DataId, mat);
            return dict;
        }
    }
    #endregion

    #endregion
}