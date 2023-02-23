using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Spiders : MonoBehaviour
{
    public float startSpeed = 10.0f;

    public float speed;
    public float starHealth = 100;
    private float health;

    public GameObject DeathEffect;

    private bool isDead = false;    

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        health = starHealth; 
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0 && !isDead)
        {
            Die(); 
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1.0f - pct);     
    }

    void Die()
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5.0f);

        //EnemyWaves.spidersAlive--;

        Destroy(gameObject); 
    }
}
