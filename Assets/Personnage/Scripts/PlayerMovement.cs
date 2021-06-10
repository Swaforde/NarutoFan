using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject instancePoint;
    public GameObject target;
    public GameObject kunai;
    public float force;

    public float speed;
    public float rangeRaycast;
    public float gravityStrength;
    public float jumpForce;

    public Transform Player;

    public Rigidbody rb;

    private Animator anim;

    bool walk;
    public bool canDoubleJump;
    public bool canDoubleJump1;
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
        Turn();
        KunaiFire();
    }

    public void Turn()
    {

        Vector3 gravityS = new Vector3(0, gravityStrength, 0);
        Physics.gravity = gravityS;

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

        if (Input.GetButtonDown("Jump") && walk == true && grounded == true)
        {
            Jump();
            anim.SetBool("Jump", true);
            canDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && walk == false && grounded == true)
        {
            Jump();
            anim.SetBool("Jump", true);
            canDoubleJump1 = true;
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump1)
        {
            Jump();
            canDoubleJump1 = false;
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

        RayCastJump();

    }

    void Jump() {
        rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    void RayCastJump()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, ray.direction * rangeRaycast, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "ground" && hit.distance <= rangeRaycast)
            {
                grounded = true;
                anim.SetBool("Jump", false);
                anim.SetBool("DoubleJump", false);
            }
            else
            {
                anim.SetBool("Jump", true);
                grounded = false;
            }
        }
    }

    void KunaiFire() {

        instancePoint.transform.LookAt(target.gameObject.transform);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(targetPos);

            StartCoroutine(test());
        }
    }

    IEnumerator test() {
        yield return new WaitForSeconds(0.0001f);

        print("fire");
        GameObject kunaiInstance = Instantiate(kunai, instancePoint.transform.position, instancePoint.transform.rotation);
        kunaiInstance.GetComponent<Rigidbody>().velocity = instancePoint.transform.forward * force;
    }

}
