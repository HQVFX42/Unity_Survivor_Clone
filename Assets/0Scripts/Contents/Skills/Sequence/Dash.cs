using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SequenceSkill
{
    Rigidbody2D _rb;
    Coroutine _coroutine;

    private void Awake()
    {
        SkillType = Define.ESkillType.Dash;
        AnimationName = "Dash";
    }

    public override void DoSkill(Action callback = null)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(CoDash(callback));
    }

    float WaitTime { get; } = 1.0f;
    float Speed { get; } = 10.0f;

    IEnumerator CoDash(Action callback = null)
    {
        transform.GetChild(0).GetComponent<Animator>().Play(AnimationName);

        _rb = GetComponent<Rigidbody2D>();
        float elapsed = 0.0f;

        Vector3 direction;
        Vector2 targetPosition = Managers.Game.Player.CenterPosition;

        //GameObject obj = Managers.Resource.Instantiate("SkillRange", pooling: true);
        //obj.transform.SetParent(transform);
        //obj.transform.localPosition = Vector3.zero;
        //SkillRange skillRange = obj.GetOrAddComponent<SkillRange>();

        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed > 2.0f) //SkillData.Duration
            {
                break;
            }

            direction = ((Vector2)Managers.Game.Player.transform.position - _rb.position);
            targetPosition = Managers.Game.Player.transform.position + direction.normalized * UnityEngine.Random.Range(1.0f, 2.0f);    //SkillData.MaxRange;

            //skillRange.SetInfo(direction, targetPosition, Vector3.Distance(_rb.position, targetPosition));

            yield return null;
        }

        //Managers.Resource.Destroy(obj);


        while (Vector3.Distance(_rb.position, targetPosition) > 0.3f)
        {
            Vector2 dirVec = targetPosition - _rb.position;

            Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + nextVec);

            yield return null;
        }
        yield return new WaitForSeconds(WaitTime);

        callback?.Invoke();
    }
}
