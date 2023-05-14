using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float disappear = 3.0f;

    float timer;

    void Awake()
    {
        timer = disappear;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Count down time after player throw the projectile away
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        // Using the physic system of Unity Engine
        rigidbody2D.AddForce(direction * force);

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        // The projectile disappear if contact with Rigidbody object
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // The projectile dissappear if contact with Enemy
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy) Destroy(gameObject);

        BossController boss = other.GetComponent<BossController>();
        if(boss) Destroy(gameObject);
    }
}
