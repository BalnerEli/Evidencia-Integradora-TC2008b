using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//controlador del player
public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;
    public GameObject loseTextObject;

    public float normalSpeed = 5f;
    public float slowSpeed = 2f;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float currentSpeed;

    // Inicializa la vida del jugador, junto con su texto de vida y vel inicial
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        loseTextObject.SetActive(false);
        currentSpeed = normalSpeed;
    }

    // Maneja el movimiento y disparos de Player
    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    // Controla el movimiento del Player
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        transform.Translate(movement * currentSpeed * Time.deltaTime);
    }

    // Controla el disparo del Player
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    // control de vida del player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Actualiza el texto de vida del player
    void UpdateHealthText()
    {
        healthText.text = "Player's life: " + currentHealth;
    }

    // destuye al Player si muere
    void Die()
    {
        loseTextObject.SetActive(true);
        Destroy(gameObject);
    }
}
