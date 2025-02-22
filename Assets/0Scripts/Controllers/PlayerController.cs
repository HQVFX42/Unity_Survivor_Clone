using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDirection;
    float _speed = 5.0f;

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

        Vector3 dir = _moveDirection * _speed * Time.deltaTime;
        transform.position += dir;
    }
}
