using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    private Transform target;

    public float range = 10f;
    public float attackRate = 1f;
    private float attackCountdown = 0f;

    public string enemyTag = "Enemy";

    public GameObject attackPrefab;
    public Transform attackPoint;
    public Transform shopPrefab;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    } 

    void UpdateTarget() {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies){

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }
        else {
            target = null;
        }
    }

    
    void Update()
    {
        if (target == null) {
            return;
        }

        if (attackCountdown <= 0f) {
            Shoot();
            attackCountdown = 1f/attackRate;
        }

        attackCountdown -= Time.deltaTime;
    }

    void Shoot() {
        GameObject attackGO =  (GameObject)Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);
        Attack attack = attackGO.GetComponent<Attack>();

        if (attack != null) {
            attack.Seek(target);
        }

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
