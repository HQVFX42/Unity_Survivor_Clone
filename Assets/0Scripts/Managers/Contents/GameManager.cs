using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    //public PlayerController Player { get { return Managers.Object?.Player; } }

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
