using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public Rigidbody2D _rigidBody { get; set; }
    public Animator _animator { get; set; }

    public float Hp { get; set; } = 100;
    public float MaxHp { get; set; } = 100;
    public virtual float MoveSpeedRate { get; set; } = 1;
    public virtual float MoveSpeed { get; set; } = 2.0f;

    public SkillHandler Skills { get; protected set; }

    private Collider2D _offset;
    public Vector3 CenterPosition
    {
        get
        {
            return _offset.bounds.center;
        }
    }
    public float ColliderRadius { get; set; }

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

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        Skills = gameObject.GetOrAddComponent<SkillHandler>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _offset = GetComponent<Collider2D>();
        ColliderRadius = GetComponent<CircleCollider2D>().radius;

        return true;
    }

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
        CreatureState = Define.ECreatureState.Dead;
    }

    public bool IsMonster()
    {
        switch (ObjectType)
        {
            case Define.EObjectType.Boss:
            case Define.EObjectType.Monster:
            case Define.EObjectType.EliteMonster:
                return true;
            case Define.EObjectType.Player:
            case Define.EObjectType.Projectile:
                return false;
            default:
                return false;
        }
    }
}
