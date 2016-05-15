using UnityEngine;
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
		if (Input.GetMouseButtonUp (0)) {
			NetworkInstanceId netId = GetComponent<NetworkIdentity> ().netId;
			CmdThrowPokeball (netId);
			print (throwPower);
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
