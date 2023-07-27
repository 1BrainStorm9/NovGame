using PixelCrew.Components.GoBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player : Entity
{
    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int AttackKey = Animator.StringToHash("attack");
    private static readonly int Hit = Animator.StringToHash("hit");
    [SerializeField] protected SpawnListComponent _particles;


    public bool isRun;

    private void FixedUpdate()
    {
        Animator.SetBool(IsRunning, isRun);
    }

    public override double Attack(Entity enemy, float multiplyDamageCoefficient)
    {
        AnimationAttack();
        return base.Attack(enemy, multiplyDamageCoefficient);
    }

    private void AnimationAttack()
    {
        Animator.SetTrigger(AttackKey);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Animator.SetTrigger(Hit);
    }

    public void OnAttack()
    {
        _particles.Spawn("Slash");
    }
}
