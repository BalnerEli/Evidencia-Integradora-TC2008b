using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase de las balas del jefe
public class BossBullet : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la bala
    public int damage = 10; // Daño que causa la bala

    // Balas con tag para conteo
    void Start()
    {
        if (gameObject.tag != "Bullet")
        {
            gameObject.tag = "Bullet";
        }
    }

    //  Dispara bala
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // Detecta colisiones con el Player para disminuir vida
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
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
