using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunai : MonoBehaviour
{
    public GameObject projectile;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            print("test");
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * 10;
        }
    }
}
