using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    public bool isDestroyed;

    public Vector3 respawnPoint;
    public Soldier[] soldierPrefabs;    

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        Renderer renderer = this.GetComponent<Renderer>();
        int r = Random.Range(0, soldierPrefabs.Length);
        Soldier soldier = GameObject.Instantiate(soldierPrefabs[r], respawnPoint, transform.rotation);
        soldier.SetTeam(team, renderer.material);    
    }


    void OnDrawGizmos()
    {
        Color c = team == Team.Blue ? Color.blue : Color.red;
        Gizmos.color = new Color(c.r, c.g, c.b, 0.5f);
        Gizmos.DrawSphere(respawnPoint, 1f);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
