using System.Security.Cryptography;
using UnityEngine;

public class MoveBehaviour : GenericBehaviour
{
	public float runSpeed = 1.0f;
	public float jumpSlowForce;
	public float jumpForce;
	//public Animator anim;
	public bool isGrounded;
	public bool canMove;
	bool walk;
	Rigidbody rb;

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "ground")
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "ground")
		{
			isGrounded = false;;
		}
	}


	void Start()
	{
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		canMove = true;
		//anim = gameObject.GetComponent<Animator>();
		rb = gameObject.GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;

	}

	void Update()
	{
		JumpManagement();
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);
	}

	void JumpManagement()
	{
		
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && walk == true)
		{
			rb.AddForce(new Vector3(0, jumpSlowForce, 0), ForceMode.Impulse);
			//anim.SetBool("Jump", true);
		}
		else {
			//anim.SetBool("Jump", false);
		}

		if (Input.GetKeyDown(KeyCode.Space)&& isGrounded == true && walk == false) {
			rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
			//anim.SetBool("simpleJump", true);
		}
		//else anim.SetBool("simpleJump", false);
	}

	void MovementManagement(float horizontal, float vertical)
	{
		Rotating(horizontal, vertical);

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)){
			if (canMove == true) {
				rb.velocity = transform.forward * Time.deltaTime * 5000;
				walk = true;
				//anim.SetBool("Sprint", true);
			}
		}else{
			walk = false;
			//anim.SetBool("Sprint", false);
		}
	}

	Vector3 Rotating(float horizontal, float vertical)
	{
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;

	}
}
