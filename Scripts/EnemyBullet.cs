using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//clase pra las balas del enemigo
public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    //balas con tag para conteo
    void Start()
    {
        if (gameObject.tag != "Bullet")
        {
            gameObject.tag = "Bullet";
        }
    }

    // dispara bala
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // Disminuye daño al Player
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
