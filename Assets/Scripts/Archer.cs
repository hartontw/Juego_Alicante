using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Soldier
{    
    public Projectile projectilePrefab;

    protected override void Attack(Actor target)
    {
        base.Attack(target);
        Projectile projectile = GameObject.Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Init(this);
    }
}
