using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startHealth = 100;
    private float health;
    public int damage = 20;
    public float speed = 3f;
    public float range = 5f;
    public float attackRate = 1f;
    
    public GameObject parEffect;
    
    private float attackCountdown = 0f;
    public int reward = 50;
    public string targetTag = "Target";
    
    public Image healthBar;
    public Rigidbody rb;
    
    
    private Transform wayP;
    private int wavepointIndex = 0;

    void Start() {
        wayP = Waypoints.points[0];
        health = startHealth;
    }

    void Update() {
        Vector3 dir = wayP.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, wayP.position) <= 0.2f) {
            GetNextWaypoint();
        }
        
        if (attackCountdown <= 0f) {
            EnemyAttack();
            attackCountdown = 1f/attackRate;
        }

        attackCountdown -= Time.deltaTime;
    }

    
    void GetNextWaypoint() {
        if (wavepointIndex >= Waypoints.points.Length -1) {
            speed = 0f;
            return;
        }
        
        wavepointIndex++;
        wayP = Waypoints.points[wavepointIndex];
    }

    void EnemyAttack()
    {
        GameObject targetToHit = GameObject.FindGameObjectWithTag(targetTag);

        float distanceToTarget = Vector3.Distance(transform.position, targetToHit.transform.position);
        
        Target tar = targetToHit.GetComponent<Target>();

        if (distanceToTarget <= range)
        {
            
            tar.TakeDamageTarget(damage);
            rb.isKinematic = false;
        }
    }
    
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void TakeDamageEnemy(int amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += reward;
        Destroy(gameObject);
        Instantiate(parEffect, transform.position, parEffect.transform.rotation);
    }

    void Damage(Transform Target)
    {
        Target tar = Target.GetComponent<Target>();

        if (tar != null)
        {
            tar.TakeDamageTarget(damage);
        }
    }
    
}
