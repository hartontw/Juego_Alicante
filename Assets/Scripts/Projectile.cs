using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
{
    private float impulse;
    private float hitDamage;
    public void Init(Archer maker)
    {
        Material makerMaterial = maker.GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = makerMaterial;
        Physics.IgnoreCollision(GetComponent<Collider>(), maker.Collider);
        hitDamage = maker.hitDamage;
        impulse = maker.attackRange * 3f;
        GetComponent<Rigidbody>().AddForce(transform.forward * impulse, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {        
        Actor actor = collision.collider.GetComponent<Actor>();
        if (actor is not null)
        {
            actor.ReceiveDamage(hitDamage);
        }
        Destroy(gameObject);
    }
}
