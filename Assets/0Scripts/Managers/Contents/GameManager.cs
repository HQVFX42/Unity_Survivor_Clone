using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    //public PlayerController Player { get { return Managers.Object?.Player; } }

    #region Currency
    public int Gold {  get; set; }
    public int Gem { get; set; }
    #endregion

    #region Movement
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
    #endregion
}
