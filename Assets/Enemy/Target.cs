using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float startHealth = 500;
    private float health;
    public Image healthBar;
    public void TakeDamageTarget(int amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            PlayerStats.Lose = true;
        }
    }

    void Start() {
        health = startHealth;
    }
}
