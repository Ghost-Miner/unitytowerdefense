using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;

    private Transform target;
    private int waypointIndex = 0;

    public static bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.waypoints[0];
        //Debug.Log("Enemy.cs |  HP: " + health);
    }

    public void TakeDamage (int amount)
    {
        health -= amount;
        //Debug.Log("Enemy.cs | damage: " + amount);
        //Debug.Log("Enemy.cs | HP: " + health);

        if (health <= 0)
        {
            Die();
            //Debug.Log("Enemy.cs | enemy died");
        }
    }

    public void Die()
    {
        WaveManager.enemiesAlive--;
        PlayerStats.money += 10;

        Destroy(gameObject);
        
        //Debug.Log("Enemy.cs | count: " + WaveSpawner.enemiesAlive);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x >= 49)
        {
            PathEnd();
        }

        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.75f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            PathEnd();
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void PathEnd ()
    {
        PlayerStats.lives--;
        WaveManager.enemiesAlive--;

        //Debug.Log("Enemy.cs | count: " + WaveSpawner.enemiesAlive);

        Destroy(gameObject);
    }
}
