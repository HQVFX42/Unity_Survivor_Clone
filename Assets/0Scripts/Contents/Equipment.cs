using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Equipment
{
    public string key = "";

    public Data.EquipmentData EquipmentData;

    public int Level { get; set; } = 1;
    public int AttackBonus { get; set; } = 0;
    public int MaxHpBonus { get; set; } = 0;

    bool _isEquipped = false;
    public bool IsEquipped
    {
        get
        {
            //장비중인 아이템인지 확인
            return _isEquipped;
        }
        set
        {
            _isEquipped = value;
        }
    }

    public bool IsOwned { get; set; } = false;
    public bool IsUpgradable { get; set; } = false;
    public bool IsConfirmed { get; set; } = false; // 장비 획득을 확인했는지
    public bool IsEquipmentSynthesizable { get; set; } = false; // 장비가 합성가능한지
    public bool IsSelected { get; set; } = false; // 합성팝업에서 선택 되어있는지
    public bool IsUnavailable { get; set; } = false; // 합성팝업에서 선택 불가능한지

    public Equipment(string key)
    {
        this.key = key;

        EquipmentData = Managers.Data.EquipDataDic[key];

        SetInfo(Level);
        //AttackBonus = EquipmentData.AtkDmgBonus + Level * EquipmentData.AtkDmgBonusPerUpgrade;
        //MaxHpBonus = EquipmentData.MaxHpBonus + Level * EquipmentData.MaxHpBonusPerUpgrade;
        //this.IsEquipped = IsEquipped;
        IsOwned = true;
    }

    public void SetInfo(string key)
    {

    }

    public void SetInfo(int level)
    {
        Level = level;

        AttackBonus = EquipmentData.AtkDmgBonus + (Level - 1) * EquipmentData.AtkDmgBonusPerUpgrade;
        MaxHpBonus = EquipmentData.MaxHpBonus + (Level - 1) * EquipmentData.MaxHpBonusPerUpgrade;
    }
}
