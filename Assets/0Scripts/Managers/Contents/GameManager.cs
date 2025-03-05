using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Playables;
using Newtonsoft.Json;
using static Define;
using System.IO;
using UnityEngine.TextCore.Text;
using System.Linq;

[Serializable]
public class StageClearInfo
{
    public int StageIndex = 1;
    public int MaxWaveIndex = 0;
    public bool isOpenFirstBox = false;
    public bool isOpenSecondBox = false;
    public bool isOpenThirdBox = false;
    public bool isClear = false;
}

[Serializable]
public class MissionInfo
{
    public int Progress;
    public bool IsRewarded;
}

[Serializable]
public class ContinueData
{
    //public bool isContinue { get { return SavedBattleSkill.Count > 0; } }
    public int PlayerDataId;
    public float Hp;
    public float MaxHp;
    public float MaxHpBonusRate = 1;
    public float HealBonusRate = 1;
    public float HpRegen;
    public float Atk;
    public float AttackRate = 1;
    public float Def;
    public float DefRate;
    public float MoveSpeed;
    public float MoveSpeedRate = 1;
    public float TotalExp;
    public int Level = 1;
    public float Exp;
    public float CriRate;
    public float CriDamage = 1.5f;
    public float DamageReduction;
    public float ExpBonusRate = 1;
    public float SoulBonusRate = 1;
    public float CollectDistBonus = 1;
    public int KillCount;
    public int SkillRefreshCount = 3;
    public float SoulCount;

    public List<SupportSkillData> SoulShopList = new List<SupportSkillData>();
    public List<SupportSkillData> SavedSupportSkill = new List<SupportSkillData>();
    public Dictionary<Define.ESkillType, int> SavedBattleSkill = new Dictionary<Define.ESkillType, int>();

    public int WaveIndex;

    public void Clear()
    {
        // 각 변수의 초기값 설정
        PlayerDataId = 0;
        Hp = 0f;
        MaxHp = 0f;
        MaxHpBonusRate = 1f;
        HealBonusRate = 1f;
        HpRegen = 0f;
        Atk = 0f;
        AttackRate = 1f;
        Def = 0f;
        DefRate = 0f;
        MoveSpeed = 0f;
        MoveSpeedRate = 1f;
        TotalExp = 0f;
        Level = 1;
        Exp = 0f;
        CriRate = 0f;
        CriDamage = 1.5f;
        DamageReduction = 0f;
        ExpBonusRate = 1f;
        SoulBonusRate = 1f;
        CollectDistBonus = 1f;

        KillCount = 0;
        SoulCount = 0f;
        SkillRefreshCount = 3;

        SoulShopList.Clear();
        SavedSupportSkill.Clear();
        SavedBattleSkill.Clear();
    }
}

public class GameData
{
    public int UserLevel = 1;
    public string UserName = "Player";

    public int Stamina = Define.MAX_STAMINA;
    public int Gold = 0;
    public int Dia = 0;

    public List<Character> Characters = new List<Character>();
    public List<Equipment> OwnedEquipments = new List<Equipment>();

    public ContinueData ContinueInfo = new ContinueData();
    public StageData CurrentStage = new StageData();

    public Dictionary<int, int> ItemDictionary = new Dictionary<int, int>();    //<ID, 갯수>
    public Dictionary<Define.EEquipmentType, Equipment> EquippedEquipments = new Dictionary<Define.EEquipmentType, Equipment>();
    public Dictionary<int, StageClearInfo> DicStageClearInfo = new Dictionary<int, StageClearInfo>();
    public Dictionary<EMissionTarget, MissionInfo> DicMission = new Dictionary<EMissionTarget, MissionInfo>()
    {
        {EMissionTarget.StageEnter, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.StageClear, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.EquipmentLevelUp, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.OfflineRewardGet, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.EquipmentMerge, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.MonsterKill, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.EliteMonsterKill, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.GachaOpen, new MissionInfo() { Progress = 0, IsRewarded = false }},
        {EMissionTarget.ADWatchIng, new MissionInfo() { Progress = 0, IsRewarded = false }},
    };
}

public class GameManager
{
    #region Game Data

    public GameData _gameData = new GameData();

    public List<Equipment> OwnedEquipments
    {
        get { return _gameData.OwnedEquipments; }
        set
        {
            _gameData.OwnedEquipments = value;
            //갱신이 빈번하게 발생하여 렉 발생, Sorting시 무한루프 발생으로 인하여 주석처리
            //EquipInfoChanged?.Invoke();
        }
    }
    public List<SupportSkillData> SoulShopList
    {
        get { return _gameData.ContinueInfo.SoulShopList; }
        set
        {
            _gameData.ContinueInfo.SoulShopList = value;
            //SaveGame();
        }
    }

    public Dictionary<int, int> ItemDictionary
    {
        get { return _gameData.ItemDictionary; }
        set
        {
            _gameData.ItemDictionary = value;
        }
    }

    public Dictionary<Define.EMissionTarget, MissionInfo> DicMission
    {
        get { return _gameData.DicMission; }
        set
        {
            _gameData.DicMission = value;
        }
    }

    public Dictionary<Define.EEquipmentType, Equipment> EquippedEquipments
    {
        get { return _gameData.EquippedEquipments; }
        set
        {
            _gameData.EquippedEquipments = value;
            EquipInfoChanged?.Invoke();
        }
    }

    public Dictionary<int, StageClearInfo> DicStageClearInfo
    {
        get { return _gameData.DicStageClearInfo; }
        set
        {
            _gameData.DicStageClearInfo = value;
            Managers.Achievement.StageClear();
            SaveGame();
        }
    }

    public ContinueData ContinueInfo
    {
        get { return _gameData.ContinueInfo; }
        set
        {
            _gameData.ContinueInfo = value;
        }
    }

    public StageData CurrentStageData
    {
        get { return _gameData.CurrentStage; }
        set { _gameData.CurrentStage = value; }
    }

    public WaveData CurrentWaveData
    {
        get { return CurrentStageData.WaveArray[CurrentWaveIndex]; }
    }

    public int CurrentWaveIndex
    {
        get { return _gameData.ContinueInfo.WaveIndex; }
        set { _gameData.ContinueInfo.WaveIndex = value; }
    }
    //public Map CurrentMap { get; set; }

    #endregion

    #region Player

    public PlayerController Player { get; set; }
    Vector2 _moveDirection;
    public Vector2 MoveDirection
    {
        get { return _moveDirection; }
        set
        {
            _moveDirection = value.normalized;
            OnMoveDirectionChanged?.Invoke(_moveDirection);
        }
    }
    #endregion

    #region Action
    public event Action<Vector2> OnMoveDirectionChanged;
    public event Action EquipInfoChanged;
    public event Action OnResourcesChagned;
    #endregion

    public float TimeRemaining = 60;
    public Vector3 SoulDestination { get; set; }
    public bool IsLoaded = false;
    public bool IsGameEnd = false;
    public CameraController CameraController { get; set; }

    #region Currency
    public int Gold { get; set; }
    public int Gem { get; set; }
    #endregion

    #region Battle

    int _killCount;
    public event Action<int> OnKillCountChanged;

    public int KillCount
    {
        get { return _killCount; }
        set
        {
            _killCount = value;
            OnKillCountChanged?.Invoke(value);
        }
    }

    #endregion

    public void SetNextStage()
    {
        CurrentStageData = Managers.Data.StageDic[CurrentStageData.StageIndex + 1];
    }

    public int GetMaxStageIndex()
    {
        foreach (StageClearInfo clearInfo in _gameData.DicStageClearInfo.Values)
        {
            if (clearInfo.MaxWaveIndex != 10)
                return clearInfo.StageIndex;
        }
        return 0;
    }

    public int GetMaxStageClearIndex()
    {
        int MaxStageClearIndex = 0;

        foreach (StageClearInfo stageClearInfo in Managers.Game.DicStageClearInfo.Values)
        {
            if (stageClearInfo.isClear == true)
                MaxStageClearIndex = Mathf.Max(MaxStageClearIndex, stageClearInfo.StageIndex);
        }
        return MaxStageClearIndex;
    }

    public void AddMaterialItem(int id, int quantity)
    {
        if (ItemDictionary.ContainsKey(id))
        {
            ItemDictionary[id] += quantity;
        }
        else
        {
            ItemDictionary[id] = quantity;
        }
        SaveGame();
    }

    public void RemovMaterialItem(int id, int quantity)
    {
        if (ItemDictionary.ContainsKey(id))
        {
            ItemDictionary[id] -= quantity;
            SaveGame();
        }
    }

    public void ExchangeMaterial(MaterialData data, int count)
    {
        switch (data.MaterialType)
        {
            case EMaterialType.Dia:
                //Dia += count;
                break;
            case EMaterialType.Gold:
                //Gold += count;
                break;
            case EMaterialType.Stamina:
                //Stamina += count;
                break;
            case EMaterialType.BronzeKey:
            case EMaterialType.SilverKey:
            case EMaterialType.GoldKey:
                //AddMaterialItem(data.DataId, count);
                break;
            case EMaterialType.RandomScroll:
                int randScroll = UnityEngine.Random.Range(50101, 50106);
                //AddMaterialItem(randScroll, count);
                break;
            case EMaterialType.WeaponScroll:
                //AddMaterialItem(Define.ID_WEAPON_SCROLL, count);
                break;
            case EMaterialType.GlovesScroll:
                //AddMaterialItem(Define.ID_GLOVES_SCROLL, count);
                break;
            case EMaterialType.RingScroll:
                //AddMaterialItem(Define.ID_RING_SCROLL, count);
                break;
            case EMaterialType.BeltScroll:
                //AddMaterialItem(Define.ID_BELT_SCROLL, count);
                break;
            case EMaterialType.ArmorScroll:
                //AddMaterialItem(Define.ID_ARMOR_SCROLL, count);
                break;
            case EMaterialType.BootsScroll:
                //AddMaterialItem(Define.ID_BOOTS_SCROLL, count);
                break;
            default:
                //TODO 
                break;
        }
    }

    #region Equipment

    public void SetBaseEquipment()
    {
        //초기아이템 설정
        Equipment weapon = new Equipment(WEAPON_DEFAULT_ID);
        Equipment gloves = new Equipment(GLOVES_DEFAULT_ID);
        Equipment ring = new Equipment(RING_DEFAULT_ID);
        Equipment belt = new Equipment(BELT_DEFAULT_ID);
        Equipment armor = new Equipment(ARMOR_DEFAULT_ID);
        Equipment boots = new Equipment(BOOTS_DEFAULT_ID);

        OwnedEquipments = new List<Equipment>
            {
                weapon,
                gloves,
                ring,
                belt,
                armor,
                boots
            };

        EquippedEquipments = new Dictionary<EEquipmentType, Equipment>();
        EquipItem(EEquipmentType.Weapon, weapon);
        EquipItem(EEquipmentType.Gloves, gloves);
        EquipItem(EEquipmentType.Ring, ring);
        EquipItem(EEquipmentType.Belt, belt);
        EquipItem(EEquipmentType.Armor, armor);
        EquipItem(EEquipmentType.Boots, boots);
    }

    public void EquipItem(EEquipmentType type, Equipment equipment)
    {
        if (EquippedEquipments.ContainsKey(type))
        {
            EquippedEquipments[type].IsEquipped = false;
            EquippedEquipments.Remove(type);
        }

        // 새로운 장비를 착용
        EquippedEquipments.Add(type, equipment);
        equipment.IsEquipped = true;
        equipment.IsConfirmed = true;

        // 장비변경 이벤트 호출
        EquipInfoChanged?.Invoke();
    }

    public void UnEquipItem(Equipment equipment)
    {
        // 착용중인 장비를 제거한다.
        if (EquippedEquipments.ContainsKey(equipment.EquipmentData.EquipmentType))
        {
            EquippedEquipments[equipment.EquipmentData.EquipmentType].IsEquipped = false;
            EquippedEquipments.Remove(equipment.EquipmentData.EquipmentType);

        }
        // 장비변경 이벤트 호출
        EquipInfoChanged?.Invoke();
    }

    public Equipment AddEquipment(string key)
    {
        if (key.Equals("None"))
            return null;

        Equipment equip = new Equipment(key);
        equip.IsConfirmed = false;
        OwnedEquipments.Add(equip);
        EquipInfoChanged?.Invoke();

        return equip;
    }

    public Equipment MergeEquipment(Equipment equipment, Equipment mergeEquipment1, Equipment mergeEquipment2, bool isAllMerge = false)
    {
        equipment = OwnedEquipments.Find(equip => equip == equipment);
        if (equipment == null)
            return null;
        mergeEquipment1 = OwnedEquipments.Find(equip => equip == mergeEquipment1);
        if (mergeEquipment1 == null)
            return null;

        if (mergeEquipment2 != null)
        {
            mergeEquipment2 = OwnedEquipments.Find(equip => equip == mergeEquipment2);
            if (mergeEquipment2 == null)
                return null;
        }

        int level = equipment.Level;
        bool isEquipped = equipment.IsEquipped;// || mergeEquipment1.IsEquipped || mergeEquipment2.IsEquipped;
        string mergedItemCode = equipment.EquipmentData.MergedItemCode;
        Equipment newEquipment = AddEquipment(mergedItemCode);
        newEquipment.Level = level;
        newEquipment.IsEquipped = isEquipped;

        OwnedEquipments.Remove(equipment);
        OwnedEquipments.Remove(mergeEquipment1);
        OwnedEquipments.Remove(mergeEquipment2);

        if (Managers.Game.DicMission.TryGetValue(EMissionTarget.EquipmentMerge, out MissionInfo mission))
            mission.Progress++;

        //자동합성인 경우는 SAVE게임 하지않고 다끝난후에 한번에 한다.
        if (isAllMerge == false)
            SaveGame();

        Debug.Log(newEquipment.EquipmentData.EquipmentGrade);
        return newEquipment;
    }

    public void SortEquipment(EEquipmentSortType sortType)
    {
        if (sortType == EEquipmentSortType.Grade)
        {
            //OwnedEquipments = OwnedEquipments.OrderBy(item => item.EquipmentGrade).ThenBy(item => item.Level).ThenBy(item => item.EquipmentType).ToList();
            OwnedEquipments = OwnedEquipments.OrderBy(item => item.EquipmentData.EquipmentGrade).ThenBy(item => item.IsEquipped).ThenBy(item => item.Level).ThenBy(item => item.EquipmentData.EquipmentType).ToList();

        }
        else if (sortType == EEquipmentSortType.Level)
        {
            OwnedEquipments = OwnedEquipments.OrderBy(item => item.Level).ThenBy(item => item.IsEquipped).ThenBy(item => item.EquipmentData.EquipmentGrade).ThenBy(item => item.EquipmentData.EquipmentType).ToList();
        }
    }

    public void GenerateRandomEquipment()
    {
        //N 0 
        //장비타입
        //
        EEquipmentType type = Utils.GetRandomEnumValue<EEquipmentType>();
        EGachaRarity rarity = Utils.GetRandomEnumValue<EGachaRarity>();
        EEquipmentGrade grade = Utils.GetRandomEnumValue<EEquipmentGrade>();
        string itemNum = UnityEngine.Random.Range(1, 4).ToString("D2");
        string gradeNum = ((int)grade).ToString("D2");


        string key = $"{rarity.ToString()[0]}{type.ToString()[0]}{itemNum}{gradeNum}";

        if (Managers.Data.EquipDataDic.ContainsKey(key))
        {
            AddEquipment(key);

        }
        //AddEquipment("N00101");
    }

    //public void GenerateRandomMaterials()
    //{
    //    EquipmentType type = Util.GetRandomEnumValue<EquipmentType>();
    //    GachaRarity rarity = Util.GetRandomEnumValue<GachaRarity>();
    //    EquipmentGrade grade = Util.GetRandomEnumValue<EquipmentGrade>();
    //    string itemNum = Random.Range(1, 4).ToString("D2");
    //    string gradeNum = ((int)grade).ToString("D2");


    //    string key = $"{rarity.ToString()[0]}{type.ToString()[0]}{itemNum}{gradeNum}";

    //    if (Managers.Data.EquipDataDic.ContainsKey(key))
    //    {
    //        AddEquipment(key);
    //    }
    //}

    public void GenerateRandomMaterials()
    {
        //임시
        List<Data.MaterialData> list = Managers.Data.MaterialDic.Values.ToList();
        for (int i = 0; i < 5; i++)
        {
            AddMaterialItem(list[UnityEngine.Random.Range(11, list.Count)].DataId, 30);
        }
    }


    public (int hp, int atk) GetEquipmentBonus()
    {
        int hpBonus = 0;
        int atkBonus = 0;

        foreach (KeyValuePair<EEquipmentType, Equipment> pair in EquippedEquipments)
        {
            hpBonus += pair.Value.MaxHpBonus;
            atkBonus += pair.Value.AttackBonus;
        }
        return (hpBonus, atkBonus);
    }

    #endregion


    #region Save&Load
    string _path;

    public void SaveGame()
    {
        if (Player != null)
        {
            _gameData.ContinueInfo.SavedBattleSkill = Player.Skills?.SavedBattleSkill;
            _gameData.ContinueInfo.SavedSupportSkill = Player.Skills?.SupportSkills;
        }
        string jsonStr = JsonConvert.SerializeObject(_gameData);
        File.WriteAllText(_path, jsonStr);
    }

    public bool LoadGame()
    {
        if (PlayerPrefs.GetInt("ISFIRST", 1) == 1)
        {
            string path = Application.persistentDataPath + "/SaveData.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return false;
        }

        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonConvert.DeserializeObject<GameData>(fileStr);
        if (data != null)
            _gameData = data;

        EquippedEquipments = new Dictionary<EEquipmentType, Equipment>();
        for (int i = 0; i < OwnedEquipments.Count; i++)
        {
            if (OwnedEquipments[i].IsEquipped)
            {
                EquipItem(OwnedEquipments[i].EquipmentData.EquipmentType, OwnedEquipments[i]);
            }
        }
        IsLoaded = true;
        return true;
    }

    public void ClearContinueData()
    {
        Managers.Game.SoulShopList.Clear();
        ContinueInfo.Clear();
        CurrentWaveIndex = 0;
        SaveGame();
    }

    public float GetTotalDamage()
    {
        float result = 0;

        foreach (SkillBase skill in Player.Skills.SkillList)
        {
            result += skill.TotalDamage;
        }

        return result;
    }
    #endregion
}
