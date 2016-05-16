﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerThrowBallHandler : NetworkBehaviour {

	public float minBallHoldingTime = 1;

	public float throwPowerRate = 0.25f;

	public float MinThrowPower = 5f;

	public float MaxThrowPower = 30f;

	private float throwPower = 5f;

	private GameObject[] textUI;

	private Text textObj;

	// Update is called once per frame
	void Start(){
		textUI = GameObject.FindGameObjectsWithTag("powerText");
		textObj = textUI[0].GetComponent<Text>();
		print (textObj.text);
		textObj.text = "";
	}
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetMouseButtonUp (0)) {
			NetworkInstanceId netId = GetComponent<NetworkIdentity> ().netId;
			CmdThrowPokeball (netId, throwPower);
			throwPower = MinThrowPower;
		}

		if (Input.GetMouseButton(0)){
			if (throwPower<MaxThrowPower)
				throwPower = throwPower + throwPowerRate;
		}

		int numberOfSquare = ((int)throwPower - (int)MinThrowPower)/3;
		textObj.text = "";
		for (int i = 0; i < numberOfSquare; i++) {
			textObj.text = textObj.text + "■";
		}
	}

	[Command]
	void CmdThrowPokeball(NetworkInstanceId netId, float throwPower) {
		GameObject player = NetworkServer.FindLocalObject (netId);
		GameObject pokeball = null;

		// Find if player "owns" the pokeball
		foreach (Transform child in player.transform) {
			if (child.CompareTag ("PlayPokeball")) {
				pokeball = child.gameObject;
				break;
			}
		}

		// Ignore if player do not have the pokeball
		if (pokeball == null) {
			return;
		}

		PokeballInfo info = pokeball.GetComponent<PokeballInfo> ();

		// Ignore if command comes from player not catching the ball
		if (!info.isCatchingByPlayer) {
			return;
		}

		float currentTime = Time.time;

		// Player need to hold the ball for a while before throwing it.
		Debug.Log(currentTime + " - " + info.lastPokeballCatchTime + " < " + minBallHoldingTime + ": " + (currentTime - info.lastPokeballCatchTime < minBallHoldingTime));
		if (currentTime - info.lastPokeballCatchTime < minBallHoldingTime) {
			return;
		}

		info.isCatchingByPlayer = false;
		info.lastPokeballThrownTime = currentTime;

		pokeball.GetComponent<Rigidbody>().isKinematic = false;
		pokeball.transform.parent = null;
		pokeball.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwPower, ForceMode.Impulse);
	}
}
