using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AttackBehavior : MonoBehaviour
{
    public float detectDistance = 1f;
    public Vector2 attackOffset = new Vector2(0.5f, 0.5f);
    public int attack = 1;
    public float attackRadius = 1;
    public LayerMask attackMask;
    private Animator animator;

    public void Setup(Animator animatorInstance, int attack)
    {
        animator = animatorInstance;
        this.attack = attack;
        var listener = animator.GetComponent<AnimationEventListener>();
        listener.onEventTrigger += OnAnimationEventTrigger;
    }

    private void OnDisable()
    {
        animator.ResetTrigger("attack");
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f), transform.right, detectDistance, attackMask);
        if (hit.collider != null)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            animator.ResetTrigger("attack");
        }
    }

    public void OnAttackEventTrigger()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        var hits = Physics2D.OverlapCircleAll(pos, attackRadius, attackMask);
        foreach(var hit in hits)
        {
            var healthBehavior = hit.GetComponent<HealthBehavior>();
            healthBehavior.TakeDamage(attack);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRadius);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f), transform.right * detectDistance, Color.red);
    }


    public void OnAnimationEventTrigger(string evt)
    {
        switch (evt)
        {
            case "attack":
                OnAttackEventTrigger();
                break;
        }
    }
}
