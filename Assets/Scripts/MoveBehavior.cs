﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Animator animator;
    public LayerMask groundLayer;
    public float speed;
    public bool isFlip;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        animator.SetBool("isMoving", false);
    }

    public void Setup(Animator animatorInstance, float speed)
    {
        animator = animatorInstance;
        this.speed = speed;
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.1f), Vector2.down, 0.1f, groundLayer);
        if (hit.collider != null)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            Move();
    }

    private void Move()
    {
        Vector2 movement = transform.right * speed * Time.deltaTime;
        rigid2D.MovePosition(rigid2D.position + movement);
    }
}