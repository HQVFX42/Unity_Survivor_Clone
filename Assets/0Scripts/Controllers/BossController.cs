using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        CreatureState = Define.ECreatureState.Moving;
        Hp = 10000;

        return true;
    }

    public override void UpdateAnimation()
    {
        switch (CreatureState)
        {
            case Define.ECreatureState.Idle:
                _animator.Play("Idle");
                break;
            case Define.ECreatureState.Moving:
                _animator.Play("Moving");
                break;
            case Define.ECreatureState.Skill:
                _animator.Play("Skill");
                break;
            case Define.ECreatureState.OnDamaged:
                _animator.Play("OnDamaged");
                break;
            case Define.ECreatureState.Dead:
                _animator.Play("Dead");
                break;
        }
    }

    protected override void UpdateIdle()
    {

    }

    protected override void UpdateMoving()
    {
        PlayerController pc = Managers.Object.Player;
        if (!pc.IsValid())
        {
            return;
        }

        Vector3 direction = pc.transform.position - transform.position;

        float range = 2.0f;
        if (direction.magnitude < range)
        {
            CreatureState = Define.ECreatureState.Skill;

            // _animator.runtimeAnimatorController.animationClips[0].length
            float animLength = 0.5f;
            Wait(animLength);
        }
    }


    protected override void UpdateSkill()
    {
        if (_coWait == null)
        {
            CreatureState = Define.ECreatureState.Moving;
        }
    }

    protected override void UpdateOnDamaged()
    {
        if (Hp <= 0)
        {
            CreatureState = Define.ECreatureState.Dead;
        }
        else
        {
            CreatureState = Define.ECreatureState.Moving;
        }
    }

    protected override void UpdateDead()
    {
        if (_coWait == null)
        {
            Managers.Object.Despawn(this);
        }
    }

    #region Wait Coroutine
    Coroutine _coWait;

    void Wait(float waitSeconds)
    {
        if (_coWait == null)
        {
            StopCoroutine(_coWait);
        }
        _coWait = StartCoroutine(CoWait(waitSeconds));
    }

    IEnumerator CoWait(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _coWait = null;
    }

    #endregion

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
    }

    protected override void OnDie()
    {
        CreatureState = Define.ECreatureState.Dead;

        base.OnDie();

        Wait(2.0f);
        //TODO : Spawning item
    }
}
