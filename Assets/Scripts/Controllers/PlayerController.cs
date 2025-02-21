using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        //UpdateInput();
        MovePlayer();
    }

    private void UpdateInput()
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1;
        }

        _moveDirection = moveDir.normalized;
    }

    private void MovePlayer()
    {
        _moveDirection = Managers.MoveDirection;
        Vector3 dir = _moveDirection * _speed * Time.deltaTime;
        transform.position += dir;
    }
}
