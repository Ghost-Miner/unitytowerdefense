using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public GameObject impactEffect;

    public int damage = 50;

    [SerializeField] private Enemy enemy;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BulletBorder")
        {
            Debug.Log("Bullet | hit border");
            Destroy(gameObject);
        }
    }

    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Destroy(gameObject);
            Damage(target.transform);

            //HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void Damage (Transform enemy)
    {
        Enemy _enemy = enemy.GetComponent<Enemy>();

        if (_enemy != null)
        {
            _enemy.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("Bullet.cs | Enemy is null");
        }
    }

    void HitTarget ()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        PlayerStats.money += 10;

        if (target.gameObject.CompareTag("Enemy")) 
        {
            enemy.Die();
            //enemy.TakeDamage(damage);
            Debug.Log("Bullet.cs | Onject: " + target.gameObject.name);
        }

        Destroy(target.gameObject);
    }
}
