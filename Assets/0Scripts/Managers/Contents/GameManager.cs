using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Player
    public PlayerController Player { get; set; }
    Vector2 _moveDirection;
    public Vector2 MoveDirection
    {
        get { return _moveDirection; }
        set
        {
            _moveDirection = value.normalized;
            OnMoveDirectionChanged?.Invoke(_moveDirection);
        }
    }
    #endregion

    #region Action
    public event Action<Vector2> OnMoveDirectionChanged;
    #endregion

    #region Currency
    public int Gold {  get; set; }
    public int Gem { get; set; }
    #endregion
}
