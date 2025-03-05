using Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Newtonsoft.Json;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDictionary();
}

public class DataManager
{
    public Dictionary<int, Data.CreatureData> CreatureDic { get; private set; } = new Dictionary<int, Data.CreatureData>();
    public Dictionary<int, Data.MaterialData> MaterialDic { get; private set; } = new Dictionary<int, Data.MaterialData>();
    public Dictionary<int, Data.SkillData> SkillDic { get; private set; } = new Dictionary<int, Data.SkillData>();
    public Dictionary<int, Data.SupportSkillData> SupportSkillDic { get; private set; } = new Dictionary<int, Data.SupportSkillData>();
    public Dictionary<int, Data.StageData> StageDic { get; private set; } = new Dictionary<int, Data.StageData>();
    public Dictionary<string, Data.EquipmentData> EquipDataDic { get; private set; } = new Dictionary<string, Data.EquipmentData>();
    //public Dictionary<int, Data.CreatureData> CreatureDic { get; private set; } = new Dictionary<int, Data.CreatureData>();
    //public Dictionary<int, Data.LevelData> LevelDataDic { get; private set; } = new Dictionary<int, Data.LevelData>();
    //public Dictionary<int, Data.EquipmentLevelData> EquipLevelDataDic { get; private set; } = new Dictionary<int, Data.EquipmentLevelData>();
    //public Dictionary<Define.GachaType, GachaTableData> GachaTableDataDic { get; private set; } = new Dictionary<Define.GachaType, GachaTableData>();
    //public Dictionary<int, MissionData> MissionDataDic { get; private set; } = new Dictionary<int, MissionData>();
    //public Dictionary<int, AchievementData> AchievementDataDic { get; private set; } = new Dictionary<int, AchievementData>();
    //public Dictionary<int, DropItemData> DropItemDataDic { get; private set; } = new Dictionary<int, DropItemData>();
    //public Dictionary<int, CheckOutData> CheckOutDataDic { get; private set; } = new Dictionary<int, CheckOutData>();
    //public Dictionary<int, OfflineRewardData> OfflineRewardDataDic { get; private set; } = new Dictionary<int, OfflineRewardData>();

    public void Init()
    {
        //PlayerDictionary = LoadXml<Data.PlayerDataLoader, int, Data.PlayerData>("PlayerData.xml").MakeDictionary();
        //MonsterDictionary = LoadXml<Data.MonsterDataLoader, int, Data.MonsterData>("MonsterData.xml").MakeDictionary();
        //SkillDictionary = LoadXml<Data.SkillDataLoader, int, Data.SkillData>("SkillData.xml").MakeDictionary();

        //CreatureDic = LoadJson<Data.CreatureDataLoader, int, Data.CreatureData>("CreatureData").MakeDictionary();
        //MaterialDic = LoadJson<Data.MaterialDataLoader, int, Data.MaterialData>("MaterialData").MakeDictionary();
        //SkillDic = LoadJson<Data.SkillDataLoader, int, Data.SkillData>("SkillData").MakeDictionary();
        //SupportSkillDic = LoadJson<Data.SupportSkillDataLoader, int, Data.SupportSkillData>("SupportSkillData").MakeDictionary();
        //StageDic = LoadJson<Data.StageDataLoader, int, Data.StageData>("StageData").MakeDictionary();
        //EquipDataDic = LoadJson<Data.EquipmentDataLoader, string, Data.EquipmentData>("EquipmentData").MakeDictionary();
    }

    #region Xml
    Item LoadSingleXml<Item>(string name)
    {
        XmlSerializer xs = new XmlSerializer(typeof(Item));
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
        {
            return (Item)xs.Deserialize(stream);
        }
    }

    Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
        {
            return (Loader)xs.Deserialize(stream);
        }
    }
    #endregion

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }
}