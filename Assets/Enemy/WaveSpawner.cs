using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    void Update() {
        if (countdown <= 0f) {

            StartCoroutine(SpawnWave());

            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {

        if (waveIndex >= waves.Length - 1)
        {
            if (!gameObject.scene.GetRootGameObjects().Any(go => go.GetComponents<Enemy>().Any()))
            {
                PlayerStats.Wictory = true;
                Debug.Log("Уровень пройден");
                
                this.enabled = false;
            }
        }
        else
        {
            Wave wave = waves[waveIndex + 1];

            for (int i = 0; i < wave.enemy.Count; i++)
            {
                SpawnEnemy(wave.enemy[i]);
                yield return new WaitForSeconds(1f / wave.rate);
            }

            waveIndex++;
        }
    }

    void SpawnEnemy(GameObject enemy) {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
