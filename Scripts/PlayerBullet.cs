using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase de balas del jugador
public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    // balas con tag para conteo
    void Start()
    {
        if (gameObject.tag != "Bullet")
        {
            gameObject.tag = "Bullet";
        }
    }

    // dispara
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // Hace daño a enemy y boss
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            BossController boss = collision.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    // Destruye la bala 
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
