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

        //StartProjectile();
        StartSword();

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

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        //Debug.Log($"OnDamaged: Hp = {Hp}, {damage} by {attacker.name} ");

        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 100);
    }

    //TODO: refactoring
    #region FireProjectile

    Coroutine _coFireProjectile;

    void StartProjectile()
    {
        if (_coFireProjectile != null)
        {
            StopCoroutine(_coFireProjectile);
        }

        _coFireProjectile = StartCoroutine(CoStartProjectile());
    }

    IEnumerator CoStartProjectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            ProjectileController pc = Managers.Object.Spawn<ProjectileController>(_fireSocket.position, 1);
            pc.SetInfo(1, this, (_fireSocket.position - _indicator.position).normalized);

            yield return wait;
        }
    }

    #endregion

    #region Sword

    SwordController _sword;
    void StartSword()
    {
        if (_sword.IsValid())
        {
            return;
        }

        _sword = Managers.Object.Spawn<SwordController>(_indicator.position, Define.SWORD_ID);
        _sword.transform.SetParent(_indicator);

        _sword.ActivateSkill();
    }

    #endregion
}
