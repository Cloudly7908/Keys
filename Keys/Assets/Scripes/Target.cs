using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 0f;

    public ParticleSystem DP;

    public GameObject Object;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            DP.Play();
            Die();

        }
        void Die()
        {
            gameObject.AddComponent(typeof(Rigidbody));
            Destroy(Object, 3f);

        }
    }

     void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
    }

}


