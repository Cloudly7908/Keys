using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    public float MaxSpeed = 30f;

    void start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void fixedUpdate()
    {
        if (rb.velocity.magnitude < MaxSpeed)
        {
            rb.AddForce(transform.forward * speed * 10);
            Debug.Log(
        }
    }
}
