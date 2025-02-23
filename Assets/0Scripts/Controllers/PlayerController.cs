using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDirection;

    public Vector2 MoveDirection
    {
        get
        {
            return _moveDirection;
        }
        set
        {
            _moveDirection = value.normalized;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _moveDirection = Managers.Game.MoveDir;
        _speed = 5.0f;
        Vector3 dir = _moveDirection * _speed * Time.deltaTime;
        transform.position += dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target == null)
        {
            return;
        }
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        //Debug.Log($"OnDamaged: Hp = {Hp}, {damage} by {attacker.name} ");

        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 100);
    }
}
