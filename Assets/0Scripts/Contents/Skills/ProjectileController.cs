using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : SkillController
{
    CreatureController _owner;
    Vector3 _moveDirection;
    float _speed = 10.0f;
    float _lifeSpan = 5.0f;

    public override bool Init()
    {
        base.Init();

        StartDestroy(_lifeSpan);

        return true;
    }

    public void SetInfo(int templateID, CreatureController owner, Vector3 moveDirection)
    {
        if (Managers.Data.SkillDictionary.TryGetValue(templateID, out Data.SkillData data) == false)
        {
            Debug.LogError("Cannot find skill data");
            return;
        }

        _owner = owner;
        _moveDirection = moveDirection;
        SkillData = data;
        //TODO: parse skill data
    }

    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += _moveDirection * _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
        {
            return;
        }
        if (this.IsValid() == false)
        {
            return;
        }

        mc.OnDamaged(_owner, SkillData.damage);

        StopDestroy();

        Managers.Object.Despawn(this);
    }
}