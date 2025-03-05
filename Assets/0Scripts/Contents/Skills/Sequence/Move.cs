using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SequenceSkill
{
    Rigidbody2D _rb;
    Coroutine _coroutine;
    CreatureController _owner;

    float Speed { get; } = 5.0f;

    private void Awake()
    {
        _owner = GetComponent<CreatureController>();
        AnimationName = "Moving";
    }
    public override void DoSkill(Action callback = null)
    {
        //UpdateSkillData(DataId);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(CoMove(callback));
    }

    IEnumerator CoMove(Action callback = null)
    {
        transform.GetChild(0).GetComponent<Animator>().Play(AnimationName);

        _rb = GetComponent<Rigidbody2D>();
        float elapsed = 0.0f;

        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed > 3.0f)
            {
                break;
            }

            Vector3 direction = (Managers.Game.Player.CenterPosition - _owner.CenterPosition).normalized;
            Vector2 targetPosition = Managers.Game.Player.CenterPosition + direction * UnityEngine.Random.Range(1.0f, 10.0f);
            //Vector2 targetPosition = Managers.Game.Player.CenterPosition + dir * UnityEngine.Random.Range(SkillData.MinCoverage, SkillData.MaxCoverage);

            if (Vector3.Distance(_rb.position, targetPosition) <= 0.1f)
                continue;

            Vector2 directionVector = targetPosition - _rb.position;
            Vector2 nextVector = directionVector.normalized * Speed * Time.fixedDeltaTime;
            //Vector2 nextVec = dirVec.normalized * SkillData.ProjSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + nextVector);

            yield return null;
        }

        callback?.Invoke();
    }
}
