using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    Vector2 _moveDirection;

    public Vector2 MoveDir
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
}
