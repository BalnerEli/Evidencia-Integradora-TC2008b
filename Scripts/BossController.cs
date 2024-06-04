using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform[] movePoints;
    public TextMeshProUGUI bossHealthText; 
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject winTextObject; 
    public float fireRate = 0.5f;
    public float moveAndFireRate = 1.5f;
    public float spiralFireRate = 0.05f;
    public float moveSpeed = 2f;
    public int maxBulletsOnScreen = 50;

    private float nextFire = 0f;
    private float nextSpiralFire = 0f;
    private float nextMoveAndFire = 0f;
    private float nextHeartFire = 0f;
    private float heartFireRate = 1.0f;
    private int heartFireCount = 0;
    private float modeDuration = 10f;
    private float modeTimer = 0f;
    private int currentMode = 0;
    private int currentPoint = 0;

    private float spiralAngle = 0f;

    //Parámetros iniciales de la vida del jefe
    void Start()
    {
        // Control de vida del jefe
        currentHealth = maxHealth;
        UpdateHealthText();
        // texto de Win
        winTextObject.SetActive(false); 
    }

    void Update()
    {
        // timer de modos del nivel
        modeTimer += Time.deltaTime;

        if (modeTimer > modeDuration)
        {
            modeTimer = 0f;
            currentMode = (currentMode + 1) % 4; 
        }

        // modos de ataque
        switch (currentMode)
        {
            case 0:
                FireBullet();
                break;
            case 1:
                MoveAndFire();
                break;
            case 2:
                FireSpiralPattern();
                break;
            case 3:
                FireHeartPattern();
                break;
        }
    }

    // Disparar balas en /|\
    void FireBullet()
    {
        if (Time.time >= nextFire && CountBullets() < maxBulletsOnScreen)
        {
            nextFire = Time.time + fireRate;

            Vector3[] directions = new Vector3[] { 
                new Vector3(-0.5f, -1, 0).normalized, 
                Vector3.down, 
                new Vector3(0.5f, -1, 0).normalized 
            };
            
            foreach (var direction in directions)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                bullet.tag = "Bullet"; 
            }
        }
    }

    // Mueve el jefe y dispara en círculo
    void MoveAndFire()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoints[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[currentPoint].position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % movePoints.Length;
        }

        if (Time.time >= nextMoveAndFire && CountBullets() < maxBulletsOnScreen)
        {
            nextMoveAndFire = Time.time + moveAndFireRate;
            int bulletCount = 8;
            float angleStep = 360f / bulletCount;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                bullet.tag = "Bullet"; 
            }
        }
    }

    // Dispara balas en espiral
    void FireSpiralPattern()
    {
        if (Time.time > nextSpiralFire && CountBullets() < maxBulletsOnScreen)
        {
            nextSpiralFire = Time.time + spiralFireRate;

            spiralAngle += 10f;
            if (spiralAngle >= 360f) spiralAngle -= 360f;

            Quaternion rotation = Quaternion.Euler(0, 0, spiralAngle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            bullet.tag = "Bullet"; 
        }
    }

    // Intento fallido de disparar balas en corazón
    void FireHeartPattern()
    {
        if (Time.time >= nextHeartFire && heartFireCount < 3 && CountBullets() < maxBulletsOnScreen)
        {
            nextHeartFire = Time.time + heartFireRate;
            heartFireCount++;
            int bulletCount = 80; 

            for (int i = 0; i < bulletCount; i++)
            {
                float t = (float)i / bulletCount * 2 * Mathf.PI; 
                float x = 16 * Mathf.Pow(Mathf.Sin(t), 3);
                float y = 13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t);

                Vector3 direction = new Vector3(x, y, 0).normalized;
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position + direction * 0.5f, rotation); // Ajusta la posición si es necesario
                bullet.tag = "Bullet";
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * 5f; 
                }
            }
        }
    }

    // Conteo de balas
    int CountBullets()
    {
        return GameObject.FindGameObjectsWithTag("Bullet").Length;
    }

    // Manejo de vida del jefe
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Texto de vida del jefe
    void UpdateHealthText()
    {
        bossHealthText.text = "Boss´life: " + currentHealth;
    }

    // Destroy jefe cuando muere
    void Die()
    {
        winTextObject.SetActive(true);
        Destroy(gameObject);
    }
}