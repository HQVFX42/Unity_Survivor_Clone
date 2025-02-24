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

    public override bool Init()
    {
        if (!base.Init())
        {
            return false;
        }

        _speed = 5.0f;
        
        StartProjectile();

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CollectEnv();
    }

    private void MovePlayer()
    {
        _moveDirection = Managers.Game.MoveDir;
        Vector3 dir = _moveDirection * _speed * Time.deltaTime;
        transform.position += dir;

        if (_moveDirection != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI - 90);
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void CollectEnv()
    {
        float sqrCollectRange = EnvCollectRange * EnvCollectRange;

        List<GemController> gems = Managers.Object.Gems.ToList();
        foreach (GemController gem in gems)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectRange)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }
        }

        var findGems = GameObject.Find("Grid").GetComponent<GridController>().GetAllObjects(transform.position, EnvCollectRange + 0.5f);
        //Debug.Log($"findGems: {findGems.Count} TotalGems: {gems.Count}");
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
}
