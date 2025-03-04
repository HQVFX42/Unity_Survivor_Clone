using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepeatSkill : SkillBase
{
    public float Cooldown { get; set; } = 1.0f;

    public override bool Init()
    {
        base.Init();
        return true;
    }

    #region CoSkill

    Coroutine _coSkill;

    public override void ActivateSkill()
    {
        if (_coSkill != null)
        {
            StopCoroutine(_coSkill);
        }

        _coSkill = StartCoroutine(CoStartSkill());
    }

    protected abstract void DoSkillJob();

    protected virtual IEnumerator CoStartSkill()
    {
        WaitForSeconds wait = new WaitForSeconds(Cooldown);

        while (true)
        {
            DoSkillJob();

            yield return wait;
        }
    }

    #endregion
}
