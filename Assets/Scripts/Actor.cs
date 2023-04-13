using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Actor : MonoBehaviour
{
    public Team team;
    public float health;

    public bool IsAlive { get => health > 0f; }

    public Collider Collider { get; private set;}

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider>();
    }

    public bool ReceiveDamage(float hitDamage)
    {
        if (!IsAlive) return false;

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
