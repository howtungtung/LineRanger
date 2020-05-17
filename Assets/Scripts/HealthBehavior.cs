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
    private bool isDead;

    public void Setup(int hp)
    {
        animator = GetComponent<Animator>();
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
        if (currentHP <= 0)
        {
            isDead = true;
            healthBar.gameObject.SetActive(false);
            animator.SetTrigger("dead");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 3);
        }
    }

    public bool IsLive()
    {
        return !isDead;
    }
}
