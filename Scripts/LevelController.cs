using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controla el juego en dos partes
public class LevelController : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject boss;
    public float enemyPhaseDuration = 30f;
    public float bossPhaseDuration = 30f;

    private float timer = 0f;
    private bool inBossPhase = false;

    // Inicializa el nivel activando el EnemySpawner, pase de enemigos
    void Start()
    {
        enemySpawner.SetActive(true);
        boss.SetActive(false);
    }

    // Controla el cambio de modos del nivel
    void Update()
    {
        timer += Time.deltaTime;

        // Inicia Boss phase
        if (!inBossPhase && timer > enemyPhaseDuration)
        {
            StartBossPhase();
        }

        // termina Boss phase
        if (inBossPhase && timer > bossPhaseDuration)
        {
            EndBossPhase();
        }
    }

    void StartBossPhase()
    {
        inBossPhase = true;
        timer = 0f;  
        enemySpawner.SetActive(false);
        boss.SetActive(true);
        ClearEnemyBullets();
    }

    void EndBossPhase()
    {
        Debug.Log("Boss Phase Ended");
    }

    //cuando termina la phase de enemies, se borran las balas
    void ClearEnemyBullets()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }
    }
}
