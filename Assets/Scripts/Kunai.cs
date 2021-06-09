using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    Rigidbody rb;
    Collider col;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground") {
            rb.velocity = new Vector3(0, 0, 0);
            col.enabled = false;
        }
    }
}
