using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TopDownAnimationController : AnimationController
{
    private static readonly int isWalking = Animator.StringToHash("isRun");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int attack = Animator.StringToHash("attack");
    private static readonly int isDie = Animator.StringToHash("isDie");

    private readonly float magnitudeThreshold = 0.5f;

    private HealthSystem healthSystem;

    protected override void Awake()
    {
        base.Awake();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
            healthSystem.OnDeath += Die;
        }
    }

    public void SetAnimator(Animator _animator)
    {
        animator = _animator;
        animator.speed = 1f;
    }

    private void Move(Vector2 vector)
    {
        animator.SetBool(isWalking, vector.magnitude > magnitudeThreshold);
    }

    private void Attacking(AttackSO sO)
    {
        //animator.SetTrigger(attack);
    }

    private void Hit()
    {
        animator.SetBool(isHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(isHit, false);
    }

    private void Die()
    {
        animator.SetBool(isDie, true);
        Invoke("StopAnim", 1f);
    }
    private void StopAnim()
    {
        animator.speed = 0;
    }
}
