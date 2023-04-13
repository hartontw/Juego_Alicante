using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    public float respawnFrequency = 2f;
    public int maxSoldiers = 10;
    public Vector3 respawnPoint;
    public Soldier[] soldierPrefabs;    

    private float lastSpawnTime = 0f;
    private int soldiersSpawned = 0;
    public float ElapsedSpawnTime { get => Time.time - lastSpawnTime; }

    public bool CanSpawn()
    {
        return ElapsedSpawnTime >= respawnFrequency && soldiersSpawned < maxSoldiers;
    }

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (CanSpawn()) {
            Spawn();
        }
    }

    void Spawn()
    {
        Renderer renderer = this.GetComponent<Renderer>();
        int r = Random.Range(0, soldierPrefabs.Length);
        Soldier soldier = GameObject.Instantiate(soldierPrefabs[r], respawnPoint, transform.rotation);
        soldier.SetTeam(team, renderer.material);    
        lastSpawnTime = Time.time;
        soldiersSpawned++;
    }


    void OnDrawGizmos()
    {
        Color c = team == Team.Blue ? Color.blue : Color.red;
        Gizmos.color = new Color(c.r, c.g, c.b, 0.5f);
        Gizmos.DrawSphere(respawnPoint, 1f);
    }

    public override void Die()
    {
        gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up*10f, ForceMode.Impulse);
        Collider.enabled = false;
    }
}
