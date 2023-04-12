using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Team team;
    public float health;

    public bool ReceiveDamage(float hitDamage)
    {
        health -= hitDamage;
        if (health <= 0f) 
        {
            Die();
            return true;
        }
        return false;
    }    

    public abstract void Die();
}
