using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerThrowBallHandler : NetworkBehaviour {

	public float minBallHoldingTime = 1;

	public float throwPower = 20;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			NetworkInstanceId netId = GetComponent<NetworkIdentity> ().netId;
			CmdThrowPokeball (netId);
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
