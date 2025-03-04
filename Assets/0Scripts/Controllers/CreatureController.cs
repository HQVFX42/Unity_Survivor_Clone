using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : BaseController
{
    public Rigidbody2D _rigidBody { get; set; }
    public Animator _animator { get; set; }

    protected float _speed = 1.0f;

    public float Hp { get; set; } = 100;
    public float MaxHp { get; set; } = 100;

    Define.ECreatureState _creatureState = Define.ECreatureState.Moving;
    public virtual Define.ECreatureState CreatureState
    {
        get { return _creatureState; }
        set
        {
            _creatureState = value;
            UpdateAnimation();
        }
    }

    public virtual void UpdateAnimation() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateOnDamaged() { }
    protected virtual void UpdateDead() { }

    public virtual void OnDamaged(BaseController attacker, SkillBase skill = null, float damage = 0)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDie();
        }
    }

    protected virtual void OnDie()
    {
        _rigidBody.simulated = false;
        transform.localScale = new Vector3(1, 1, 1);
        CreatureState = ECreatureState.Dead;
    }

    public bool IsMonster()
    {
        switch (ObjectType)
        {
            case EObjectType.Boss:
            case EObjectType.Monster:
            case EObjectType.EliteMonster:
                return true;
            case EObjectType.Player:
            case EObjectType.Projectile:
                return false;
            default:
                return false;
        }
    }
}
