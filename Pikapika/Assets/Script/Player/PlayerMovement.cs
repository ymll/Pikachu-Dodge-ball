using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMovement : NetworkBehaviour {

	public float speed = 7;

	public float gravity = 10;

	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}

		ApplyGravity ();
		ApplyKeyboardControl ();
	}

	private void ApplyGravity() {
		if(moveDirection.y > gravity * -1) {
			moveDirection.y -= gravity * Time.deltaTime;
		}
		controller.Move (moveDirection * Time.deltaTime);
	}

	private void ApplyKeyboardControl() {
		bool isRunning = Input.GetKey (KeyCode.LeftShift);
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * speed * (isRunning ? 2 : 1);

		if (controller.isGrounded) {
			controller.transform.Rotate (0, x, 0);
			controller.transform.Translate (0, 0, z);

			if (Input.GetButton ("Jump")) {
				moveDirection.y = speed;
			}
		} else {
			// Allow move front while jump
			if (z > 0) {
				controller.transform.Translate (0, 0, z);
			}
		}
	}
}
