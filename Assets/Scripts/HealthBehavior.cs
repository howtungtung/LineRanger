using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBehavior : MonoBehaviour
{
    public Slider healthBar;
    private int currentHP;
    private Animator animator;

    public void Setup(Animator animatorInstance, int hp)
    {
        animator = animatorInstance;
        currentHP = hp;
        healthBar.maxValue = currentHP;
        healthBar.value = currentHP;
        healthBar.gameObject.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.value = currentHP;
        animator.SetTrigger("hit");
        if(currentHP <= 0)
        {
            healthBar.gameObject.SetActive(false);
            animator.SetTrigger("dead");
        }
    }
}
