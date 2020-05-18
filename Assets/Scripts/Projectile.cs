using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    public float speed = 1f;
    public int attack;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void Setup(int layer, int attack)
    {
        gameObject.layer = layer;
        this.attack = attack;
    }

    private void FixedUpdate()
    {
        Vector2 movement = Time.deltaTime * speed * transform.right;
        rigid2D.MovePosition(rigid2D.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthBahvior = collision.GetComponent<HealthBehavior>();
        if (healthBahvior)
            healthBahvior.TakeDamage(attack);
        Destroy(gameObject);
    }
}
