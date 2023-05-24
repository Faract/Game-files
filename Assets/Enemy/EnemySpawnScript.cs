using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 3;
    private float timer = 0;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnEnemy ();
            timer = 0;
        }
       
    }

    void spawnEnemy ()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
