using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int index;
    public float maxHealth = 100;
    public float currenthealth;
    public HealthBar healthBar;
    public float laserdamage = 0.05f;

    void Start()
    {
        currenthealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currenthealth = maxHealth;
        }

        if (currenthealth <= 0)
        {
            Debug.Log("Die");

        }
    }

    public void TakeDamage(float damage)
    {
        currenthealth -= damage;

        healthBar.SetHealth((int)currenthealth);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "laser")
        {
            TakeDamage(laserdamage);
        }


    }
    
}

