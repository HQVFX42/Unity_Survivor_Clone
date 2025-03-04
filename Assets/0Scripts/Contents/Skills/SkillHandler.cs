using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using static Define;

public class SkillHandler : MonoBehaviour
{
    [SerializeField]
    private List<SkillBase> _skillList = new List<SkillBase>();
    public List<SkillBase> SkillList { get { return _skillList; } }
    public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();
    //public List<SupportSkillData> LockedSupportSkills { get; } = new List<SupportSkillData>();
    //public List<SupportSkillData> SupportSkills = new List<SupportSkillData>();

    public List<SkillBase> ActivatedSkills
    {
        get { return SkillList.Where(skill => skill.IsLearnedSkill).ToList(); }
    }

    [SerializeField]
    public Dictionary<Define.ESkillType, int> SavedBattleSkill = new Dictionary<ESkillType, int>();

    public event Action UpdateSkillUi;

    public Define.EObjectType _ownerType;

    public void Awake()
    {
        _ownerType = GetComponent<CreatureController>().ObjectType;

    }

    public void Init()
    {
        //SkillList.Clear();
        //SupportSkills.Clear();
        //ActivatedSkills.Clear();
        //SavedSkill.Clear();
    }
    public void SetInfo(Define.EObjectType type)
    {
        _ownerType = type;
    }

    public void LoadSkill(Define.ESkillType skillType, int level)
    {
        // 모든스킬은 0으로 시작. 레벨 수 만큼 레벨업
        AddSkill(skillType);
        for (int i = 0; i < level; i++)
        {
            LevelUpSkill(skillType);
        }
    }

    public void AddSkill(Define.ESkillType skillType, int skillId = 0)
    {
        string className = skillType.ToString();

        if (skillType == Define.ESkillType.None)
        {
            Debug.Log(className + " is not valid skill type");
        }
        else
        {
            SequenceSkill skill = gameObject.AddComponent(Type.GetType(className)) as SequenceSkill;
            if (skill != null)
            {
                skill.ActivateSkill();
                skill.Owner = GetComponent<CreatureController>();
                skill.DataId = skillId;
                SkillList.Add(skill);
                SequenceSkills.Add(skill);
            }
            else
            {
                RepeatSkill skillbase = gameObject.GetComponent(Type.GetType(className)) as RepeatSkill;
                SkillList.Add(skillbase);
                if (SavedBattleSkill.ContainsKey(skillType))
                {
                    SavedBattleSkill[skillType] = skillbase.Level;
                }
                else
                {
                    SavedBattleSkill.Add(skillType, skillbase.Level);
                }
            }
        }
    }

    public void LevelUpSkill(Define.ESkillType skillType)
    {
        for (int i = 0; i < SkillList.Count; i++)
        {
            if (SkillList[i].SkillType == skillType)
            {
                SkillList[i].OnLevelUp();
                if (SavedBattleSkill.ContainsKey(skillType))
                {
                    SavedBattleSkill[skillType] = SkillList[i].Level;
                }
                UpdateSkillUi?.Invoke();
            }
        }
    }
}
