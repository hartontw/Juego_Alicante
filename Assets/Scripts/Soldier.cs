using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soldier : MonoBehaviour
{
    public Team team;
    public float health;
    public float speed;
    public float hitDamage;
    public float hitsPerSecond;
    public float attackRange;

    bool ReceiveDamage(float hitDamage)
    {
        health -= hitDamage;
        if (health <= 0f) 
        {
            Die();
            return true;
        }
        return false;
    }    

    void Move(){

    }
    void Die(){}
    void Attack(){}
}
