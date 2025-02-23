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

    #endregion

    #region Json

    #region PlayerData
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class PlayerDataLoader : ILoader<int, PlayerData>
    {
        public List<PlayerData> stats = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDictionary()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
            foreach (PlayerData stat in stats)
            {
                dict.Add(stat.level, stat);
            }
            return dict;
        }
    }
    #endregion

    #endregion
}