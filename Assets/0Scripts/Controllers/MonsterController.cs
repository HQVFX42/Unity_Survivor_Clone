using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    Define.ECreatureState _creatureState = Define.ECreatureState.Moving;
    public virtual Define.ECreatureState CreatureState
    {
        get
        {
            return _creatureState;
        }
        set
        {
            _creatureState = value;
            UpdateAnimation();
        }
    }

    protected Animator _animator;
    public virtual void UpdateAnimation()
    {

    }

    public override void UpdateController()
    {
        base.UpdateController();

        switch (CreatureState)
        {
            case Define.ECreatureState.Idle:
                UpdateIdle();
                break;
            case Define.ECreatureState.Moving:
                UpdateMoving();
                break;
            case Define.ECreatureState.Skill:
                UpdateSkill();
                break;
            case Define.ECreatureState.OnDamaged:
                UpdateOnDamaged();
                break;
            case Define.ECreatureState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle()
    {

    }

    protected virtual void UpdateMoving()
    {
    }

    protected virtual void UpdateSkill()
    {
    }

    protected virtual void UpdateOnDamaged()
    {
    }

    protected virtual void UpdateDead()
    {
    }


    public override bool Init()
    {
        if (base.Init())
        {
            return false;
        }

        _animator = GetComponent<Animator>();
        ObjectType = Define.EObjectType.Monster;
        CreatureState = Define.ECreatureState.Moving;

        return true;
    }

    void FixedUpdate()
    {
        PlayerController pc = Managers.Object.Player;
        if (!pc.IsValid())
        {
            return;
        }

        if (CreatureState != Define.ECreatureState.Moving)
        {
            return;
        }

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPosition = transform.position + dir.normalized * _speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(newPosition);

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (!target.IsValid())
        {
            return;
        }
        if (!this.IsValid())    // Always check when using pooling method
        {
            return;
        }

        if (_coDotDamage != null)
        {
            StopCoroutine(_coDotDamage);
        }
        _coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (!target.IsValid())
        {
            return;
        }
        if (!this.IsValid())    // Always check when using pooling method
        {
            return;
        }

        if (_coDotDamage != null)
        {
            StopCoroutine(_coDotDamage);
        }
        _coDotDamage = null;
    }

    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target)
    {
        while (true)
        {
            target.OnDamaged(this, 1);

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDie()
    {
        base.OnDie();

        if (_coDotDamage != null)
        {
            StopCoroutine(_coDotDamage);
        }
        _coDotDamage = null;

        //TODO : Spawning item
        GemController gc = Managers.Object.Spawn<GemController>(transform.position);

        Managers.Object.Despawn(this);
    }
}
