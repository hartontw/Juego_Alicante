using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soldier : Actor
{
    public float speed;
    public float hitDamage;
    public float hitsPerSecond;
    public float attackRange;
    public float viewRange;
    private Actor target;

    private Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Behaviour();
    }

    void Behaviour()
    {
        target = SearchEnemy();
        if (target == null) {
            body.velocity = Vector3.zero;
            return;
        }
        
        RotateTowards(target.transform.position);
        if (DistanceTo(target) > attackRange) {
            Move();
        }
        else {
            Attack();
        }
    }

    public float DistanceTo(Actor target)
    {
        Collider targetCollider = target.GetComponent<Collider>();
        Vector3 targetPoint = targetCollider.ClosestPoint(transform.position);
        
        Collider collider = GetComponent<Collider>();
        Vector3 point = collider.ClosestPoint(targetPoint);

        return Vector3.Distance(targetPoint, point);
    }

    Actor SearchEnemy()
    {        
        RaycastHit[] hits = Physics.SphereCastAll(body.position, viewRange, transform.forward, Mathf.Epsilon);

        Actor desiredTarget = null;

        float minDistance = float.MaxValue;
        foreach(RaycastHit hit in hits)
        {            
            Actor target = hit.collider.GetComponent<Actor>();
            if (target != null 
                && target.team != team
                && DistanceTo(target) < minDistance)
            {
                desiredTarget = target;
            }
        }

        if (desiredTarget) {
            return desiredTarget;
        }

        Tower[] towers = FindObjectsOfType<Tower>();
        foreach(Tower tower in towers) {
            if (tower.team != team) {
                return tower;
            }
        }

        return null;
    }

    void RotateTowards(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0f;
        body.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void Move()
    {
        body.velocity = transform.forward * speed;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
    void Attack(){
    }

    public void SetTeam(Team team, Material material)
    {
        this.team = team;
        GetComponent<Renderer>().material = material;
    }
}
