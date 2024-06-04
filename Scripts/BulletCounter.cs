using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//clase para conteo de balas
public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCounterText;
    public float updateInterval = 0.5f;

    private float nextUpdate = 0f;

    // Actualiza el contador de balas 
    void Update()
    {
        if (Time.time > nextUpdate)
        {
            nextUpdate = Time.time + updateInterval;
            UpdateBulletCounter();
        }
    }

    // Actualiza balas y el texto de conteo de balas
    void UpdateBulletCounter()
    {
        int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
        bulletCounterText.text = "Bullets: " + bulletCount;
    }
}
