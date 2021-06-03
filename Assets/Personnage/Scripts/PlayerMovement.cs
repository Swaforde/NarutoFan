using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public Transform Player;

    public Rigidbody rb;

    private Animator anim;

    bool walk;
    public bool grounded;

    float h;
    float v;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {     
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * v + right * h;
        rb.velocity = new Vector3(moveDir.x * speed * Time.fixedDeltaTime, rb.velocity.y, moveDir.z * speed * Time.fixedDeltaTime);

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            walk = true;
        }
        else walk = false;

        if (Input.GetButtonDown("Jump") && walk == true)
        {
            rb.velocity = Vector3.up * jumpForce * Time.fixedDeltaTime;
        }

        if (Input.GetButtonDown("Jump") && walk == false) {
            rb.velocity = Vector3.up * jumpForce * Time.fixedDeltaTime;
        }


        if (moveDir != new Vector3(0, 0, 0))
        {
            Player.rotation = Quaternion.LookRotation(moveDir);
        }

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            anim.SetBool("Sprint", true);
        }
        else anim.SetBool("Sprint", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground") {
            grounded = true;
            anim.SetBool("Jump", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground") {
            grounded = false;
            anim.SetBool("Jump", false);
        }
    }
}
