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

    void Start()
    {
        target = Waypoints.waypoints[0];
    }

    private void OnDestroy()
    {
        WaveManager.enemiesAlive--;
        PlayerStats.money += 10;
    }

    public void TakeDamage (int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

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

        Destroy(gameObject);
    }
}
