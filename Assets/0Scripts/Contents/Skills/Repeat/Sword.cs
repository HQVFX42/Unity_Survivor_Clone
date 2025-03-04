using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : RepeatSkill
{
    [SerializeField]
    ParticleSystem[] _particles;

    protected enum ESwingType
    {
        First,
        Second,
        Third,
        Fourth,
    }

    private void Awake()
    {
        SkillType = Define.ESkillType.Sword;
    }

    public override void ActivateSkill()
    {
        StartCoroutine(CoSwingSword());
    }

    float CoolDownTime = 1.0f;

    IEnumerator CoSwingSword()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoolDownTime);

            SetParticles(ESwingType.First);
            _particles[(int)ESwingType.First].Play();
            TurnOnPhysics(ESwingType.First, true);
            yield return new WaitForSeconds(_particles[(int)ESwingType.First].main.duration);
            TurnOnPhysics(ESwingType.First, false);

            SetParticles(ESwingType.Second);
            _particles[(int)ESwingType.Second].Play();
            TurnOnPhysics(ESwingType.Second, true);
            yield return new WaitForSeconds(_particles[(int)ESwingType.Second].main.duration);
            TurnOnPhysics(ESwingType.Second, false);

            SetParticles(ESwingType.Third);
            _particles[(int)ESwingType.Third].Play();
            TurnOnPhysics(ESwingType.Third, true);
            yield return new WaitForSeconds(_particles[(int)ESwingType.Third].main.duration);
            TurnOnPhysics(ESwingType.Third, false);

            SetParticles(ESwingType.Fourth);
            _particles[(int)ESwingType.Fourth].Play();
            TurnOnPhysics(ESwingType.Fourth, true);
            yield return new WaitForSeconds(_particles[(int)ESwingType.Fourth].main.duration);
            TurnOnPhysics(ESwingType.Fourth, false);
        }
    }

    void SetParticles(ESwingType swingType)
    {
        float z = transform.parent.transform.eulerAngles.z;
        float radian = (Mathf.PI / 180) * z * -1;

        var main = _particles[(int)swingType].main;
        main.startRotation = radian;
    }

    void TurnOnPhysics(ESwingType swingType, bool simulated)
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].GetComponent<Rigidbody2D>().simulated = false;
        }

        _particles[(int)swingType].GetComponent<Rigidbody2D>().simulated = simulated;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.IsLearnedSkill == false)
        {
            return;
        }

        CreatureController creature = collision.transform.GetComponent<CreatureController>();
        if (creature?.IsMonster() == true)
        {
            creature.OnDamaged(Managers.Game.Player, this);
        }
    }

    protected override void DoSkillJob()
    {
        StartCoroutine(CoSwingSword());
    }
}
