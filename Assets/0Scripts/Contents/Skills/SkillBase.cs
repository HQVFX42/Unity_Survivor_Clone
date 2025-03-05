using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : BaseController
{
    public CreatureController Owner { get; set; }

    Define.ESkillType skillType;
    public Define.ESkillType SkillType
    {
        get
        {
            return skillType;
        }
        set
        {
            skillType = value;
        }
    }

    #region Level
    int level = 0;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    #endregion

    public Data.SkillData SkillData { get; protected set; }

    public int SkillLevel { get; set; } = 0;

    public float TotalDamage { get; set; } = 0;
    public bool IsLearnedSkill { get { return SkillLevel > 0; } }

    public int Damage { get; set; } = 100;

    public virtual void ActivateSkill()
    {

    }

    public virtual void OnLevelUp()
    {
        if (Level == 0)
        {
            ActivateSkill();
        }
        Level++;
        //UpdateSkillData();
        //Debug.Log($"@>> Skill Level up : {SkillType.ToString()} -> Level {Level}, {SkillData.DataId}");
    }

    protected virtual void GenerateProjectile(int templateID, CreatureController owner, Vector3 startPosition, Vector3 direction, Vector3 targetPosition)
    {
        ProjectileController pc = Managers.Object.Spawn<ProjectileController>(startPosition, templateID);
        pc.SetInfo(templateID, owner, direction);
    }

    #region Destroy

    Coroutine _coDestroy = null;

    public void StartDestroy(float delaySeconds)
    {
        StopDestroy();
        _coDestroy = StartCoroutine(CoDestroy(delaySeconds));
    }

    public void StopDestroy()
    {
        if (_coDestroy != null)
        {
            StopCoroutine(_coDestroy);
            _coDestroy = null;
        }
    }

    IEnumerator CoDestroy(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if (this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }
    #endregion
}
