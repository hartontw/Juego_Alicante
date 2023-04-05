using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Team team;
    public float health;

    public bool isDestroyed;

    public Vector3 respawnPoint;
    public Soldier[] soldierPrefabs;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        Soldier soldier = GameObject.Instantiate(soldierPrefabs[0], respawnPoint, transform.rotation);
        //Asignar equipo y color        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = team == Team.Blue ? Color.blue : Color.red;
        Gizmos.DrawSphere(respawnPoint, 1f);
    }
}
