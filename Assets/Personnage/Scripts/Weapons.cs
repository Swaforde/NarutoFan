using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject target;
    public GameObject kunai;
    PlayerMovement pm;

    public float force;

    private void Start()
    {

    }

    void Update()
    {
        transform.LookAt(target.gameObject.transform);

        if (Input.GetMouseButtonDown(0))
        {
            print("fire");
            pm.Turn();
            GameObject kunaiInstance = Instantiate(kunai, transform.position, transform.rotation);
            kunaiInstance.GetComponent<Rigidbody>().velocity = transform.forward * force;

        }
    }
}
