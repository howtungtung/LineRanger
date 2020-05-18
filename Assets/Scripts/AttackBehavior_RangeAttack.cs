using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior_RangeAttack : AttackBehavior
{
    public Projectile projectile;
    public Transform firePos;

    public override void OnAttackEventTrigger()
    {
        var projectileInstance = Instantiate(projectile, firePos.position, firePos.rotation);
        projectileInstance.Setup(gameObject.layer, attack);
    }
}
