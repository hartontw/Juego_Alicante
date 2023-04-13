using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Soldier : Actor
{
    public float speed;
    public float hitDamage;
    public float hitsPerSecond;
    public float attackRange;
    public float viewRange;
    private Actor target;
    private float lastAttackTime = 0f;

    public Rigidbody Body { get; private set; }

    public float AttackFrequency { get => 1f / hitsPerSecond; }
    public float LastAttackElapsedTime { get => Time.time - lastAttackTime; }

    protected override void Awake()
    {
        base.Awake();
        Body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Behaviour();
    }

    void Behaviour()
    {
        if (!IsAlive) {
            return;
        }

        target = SearchEnemy();
        if (target == null) {
            Body.velocity = Vector3.zero;
            return;
        }
        
        RotateTowards(target.transform.position);
        if (DistanceTo(target) > attackRange) {
            Move();
        }
        else if (CanAttack()) {
            Attack(target);
        }
    }

    public bool CanAttack()
    {
        return LastAttackElapsedTime >= AttackFrequency;
    }

    public float DistanceTo(Actor target)
    {
        Vector3 targetPoint = target.Collider.ClosestPoint(transform.position);
        
        Vector3 point = Collider.ClosestPoint(targetPoint);

        return Vector3.Distance(targetPoint, point);
    }

    Actor SearchEnemy()
    {        
        RaycastHit[] hits = Physics.SphereCastAll(Body.position, viewRange, transform.forward, Mathf.Epsilon);

        Actor desiredTarget = null;

        float minDistance = float.MaxValue;
        foreach(RaycastHit hit in hits)
        {            
            Actor target = hit.collider.GetComponent<Actor>();
            if (target != null  
                && target.team != team
                && target.IsAlive
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
        Body.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void Move()
    {
        Body.velocity = transform.forward * speed;
    }

    public override void Die()
    {
        Collider.enabled = false;
    }

    protected virtual void Attack(Actor target) {        
        lastAttackTime = Time.time;
    }

    public void SetTeam(Team team, Material material)
    {
        this.team = team;
        GetComponent<Renderer>().material = material;
    }
}
