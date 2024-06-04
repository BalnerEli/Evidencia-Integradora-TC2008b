using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controla el enemy
public class EnemyController : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    public float speed = 2f;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    public int bulletCount = 8;
    private float nextFire = 0f;

    // Vida del enemigo
    void Start()
    {
        currentHealth = maxHealth;
    }

    // controla movimiento y disparo 
    void Update()
    {
        // Mueve al enemy
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Dispara balas en cpirculo
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireCircularPattern();
        }
    }

    // Dispara balas en círculo
    void FireCircularPattern()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.tag = "Bullet";
        }
    }

    // Baja vida del enemigo
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // destuye enemy si muere
    void Die()
    {
        Destroy(gameObject);
    }

    // Destruye al enemy saliendo de pantalla
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
