using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Transform target;

    public int damage = 50;
    public float speed = 70f;
    public float explosionRadius = 0;
    public GameObject parEffect;

    public void Seek (Transform _target){
        target = _target;
    }
    
    
    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        Instantiate(parEffect, transform.position, transform.rotation);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
    }
    
    void Damage(Transform enemy)
    {
        Enemy en = enemy.GetComponent<Enemy>();

        if (en != null)
        {
            en.TakeDamageEnemy(damage);
        }
        
        Destroy(gameObject);
    }

    void Explode()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
       foreach (Collider collider in colliders)
       {
           if (collider.tag == "Enemy")
           {
               Damage(collider.transform);
           }
       }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
