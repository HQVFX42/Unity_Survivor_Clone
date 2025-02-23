using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Melee
// Projectile
// AOE

public class SkillController : BaseController
{
    public Define.ESkillType SkillType { get; set; }
    public Data.SkillData SkillData { get; protected set; }

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
