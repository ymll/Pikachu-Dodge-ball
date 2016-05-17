using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	//player's movement speed
	float speed = 7;

	//amount of gravitational force applied to the player
	float gravity = 10;

	// player's CharacterController component
	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		controller = transform.GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//APPLY GRAVITY
		if (moveDirection.y > gravity * -1) {
			moveDirection.y -= gravity * Time.deltaTime;
		}
		controller.Move (moveDirection * Time.deltaTime);
		var left = transform.TransformDirection (Vector3.left);

		if (controller.isGrounded) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				moveDirection.y = speed;
			} else if (Input.GetKey ("w")) {
				if (Input.GetKey (KeyCode.LeftShift)) {
					controller.SimpleMove (transform.forward * speed * 2);
				} else {
					controller.SimpleMove (transform.forward * speed);
				}
			} else if (Input.GetKey ("s")) {
				if (Input.GetKey (KeyCode.LeftShift)) {
					controller.SimpleMove (transform.forward * -speed * 2);
				} else {
					controller.SimpleMove (transform.forward * -speed);
				}
			} else if (Input.GetKey ("a")) {
				if (Input.GetKey (KeyCode.LeftShift)) {
					controller.SimpleMove (left * speed * 2);
				} else {
					controller.SimpleMove (left * speed);
				}
			} else if (Input.GetKey ("d")) {
				if (Input.GetKey (KeyCode.LeftShift)) {
					controller.SimpleMove (left * -speed * 2);
				} else {
					controller.SimpleMove (left * -speed);
				}
			}
		} else {
			if (Input.GetKey ("w")) {
				Vector3 relative = transform.TransformDirection (0, 0, 1);
				if (Input.GetKey (KeyCode.LeftShift)) {
					controller.Move (relative * Time.deltaTime * speed * 2);
				} else {
					controller.Move (relative * Time.deltaTime * speed);
				}
			}
		}
	}
}
