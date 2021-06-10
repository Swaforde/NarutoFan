using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    bool hitGround;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        hitGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground") {
            rb.velocity = new Vector3(0, 0, 0);
            col.enabled = false;
            if (hitGround == false) {
                StartCoroutine(Destroy());
                hitGround = true;
            }
        }
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(5);
        print("test");
        Destroy(gameObject);
    }
}
