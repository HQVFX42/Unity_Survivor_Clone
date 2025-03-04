using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDirection = Vector2.zero;

    float EnvCollectRange = 1.0f;

    [SerializeField]
    Transform _indicator;

    [SerializeField]
    Transform _fireSocket;

    public Transform Indicator { get { return _indicator; } }
    public Vector3 FireSocket { get { return _fireSocket.position; } }
    public Vector3 ShootDirection { get { return (_fireSocket.position - _indicator.position).normalized; } }

    //public Vector2 MoveDirection
    //{
    //    get
    //    {
    //        return _moveDirection;
    //    }
    //    set
    //    {
    //        _moveDirection = value.normalized;
    //    }
    //}

    void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnMoveDirectionChanged -= HandleOnMoveDirectionChanged;
    }

    public override bool Init()
    {
        if (!base.Init())
        {
            return false;
        }

        _speed = 5.0f;

        //event
        Managers.Game.OnMoveDirectionChanged += HandleOnMoveDirectionChanged;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CollectEnv();
    }

    void HandleOnMoveDirectionChanged(Vector2 dir)
    {
        _moveDirection = dir;
    }

    private void MovePlayer()
    {
        //_moveDirection = Managers.Game.MoveDir;
        Vector3 dir = _moveDirection * _speed * Time.deltaTime;
        transform.position += dir;

        if (_moveDirection != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void CollectEnv()
    {
        float sqrCollectRange = EnvCollectRange * EnvCollectRange;

        var findGems = GameObject.Find("Grid").GetComponent<GridController>().GetAllObjects(transform.position, EnvCollectRange + 0.5f);
        foreach (var go in findGems)
        {
            GemController gem = go.GetComponent<GemController>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectRange)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }

            Debug.Log($"Gem: {Managers.Game.Gem}, GemInRange: {findGems.Count}");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target == null)
        {
            return;
        }
    }

    public override void OnDamaged(BaseController attacker, SkillBase skill = null, float damage = 0)
    {
        base.OnDamaged(attacker);

        //Debug.Log($"OnDamaged: Hp = {Hp}, {damage} by {attacker.name} ");

        //CreatureController cc = attacker as CreatureController;
        //cc?.OnDamaged(this, 100);
    }
}
