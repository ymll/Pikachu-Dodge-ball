using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballThrowHandler : NetworkBehaviour {

	private PokeballInfo info;

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
		info.isCatchingByPlayer = false;
		info.lastPokeballThrownTime = Time.time;

		GameObject player = NetworkServer.FindLocalObject (info.touchingPlayerId);

		info.touchingPlayerId = new NetworkInstanceId();
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
		gameObject.transform.parent = null;
		gameObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwPower, ForceMode.Impulse);
	}
}
