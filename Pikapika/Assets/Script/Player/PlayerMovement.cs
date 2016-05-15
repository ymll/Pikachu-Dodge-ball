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

		if (isLocalPlayer) {
			gameObject.AddComponent<MouseLook> ();
		}
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

		if (Input.GetMouseButtonUp (0)) {
			throwPokeball ();
		}
	}

	private void throwPokeball () {
		NetworkInstanceId netId = GetComponent<NetworkIdentity> ().netId;
		CmdThrowPokeball (netId);
	}

	[Command]
	void CmdThrowPokeball(NetworkInstanceId netId) {
		GameObject player = NetworkServer.FindLocalObject (netId);
		GameObject pokeball = null;
		foreach (Transform child in player.transform) {
			if (child.CompareTag ("PlayPokeball")) {
				pokeball = child.gameObject;
				break;
			}
		}
		if (pokeball == null) {
			return;
		}

		PokeballInfo info = pokeball.GetComponent<PokeballInfo> ();

		// Ignore if command comes from player not catching the ball
		if (!info.isCatchingByPlayer || info.touchingPlayerId.Value != netId.Value) {
			return;
		}

		float minBallHoldingTime = 5;

		float throwPower = 20;

		float currentTime = Time.time;

		// Player need to hold the ball for a while before throwing it.
		Debug.Log(currentTime + " - " + info.lastPokeballCatchTime + " < " + minBallHoldingTime + ": " + (currentTime - info.lastPokeballCatchTime < minBallHoldingTime));
		if (currentTime - info.lastPokeballCatchTime < minBallHoldingTime) {
			return;
		}

		info.isCatchingByPlayer = false;
		info.lastPokeballThrownTime = currentTime;

		info.touchingPlayerId = new NetworkInstanceId();
		pokeball.GetComponent<Rigidbody>().isKinematic = false;
		pokeball.transform.parent = null;
		pokeball.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwPower, ForceMode.Impulse);
	}
}
