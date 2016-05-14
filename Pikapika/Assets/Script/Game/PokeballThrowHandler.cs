using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballThrowHandler : NetworkBehaviour {

	private PokeballInfo info;

	public float minBallHoldingTime = 5;

	public float throwPower = 20;

	// Use this for initialization
	void Start () {
		info = gameObject.GetComponent<PokeballInfo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0) && info.isCatchingByPlayer) {
			CmdThrowPokeball ();
		}
	}

	[Command]
	void CmdThrowPokeball() {
		float currentTime = Time.time;

		// Player need to hold the ball for a while before throwing it.
		Debug.Log(currentTime + " - " + info.lastPokeballCatchTime + " < " + minBallHoldingTime + ": " + (currentTime - info.lastPokeballCatchTime < minBallHoldingTime));
		if (currentTime - info.lastPokeballCatchTime < minBallHoldingTime) {
			return;
		}

		info.isCatchingByPlayer = false;
		info.lastPokeballThrownTime = currentTime;

		GameObject player = NetworkServer.FindLocalObject (info.touchingPlayerId);

		info.touchingPlayerId = new NetworkInstanceId();
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
		gameObject.transform.parent = null;
		gameObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwPower, ForceMode.Impulse);
	}
}
