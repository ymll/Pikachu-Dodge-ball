using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballCatchHandler : NetworkBehaviour {

	private PokeballInfo info;

	// Use this for initialization
	void Start () {
		info = gameObject.GetComponent<PokeballInfo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.E) && !info.isCatchingByPlayer && !info.touchingPlayerId.IsEmpty()) {
			CmdCatchPokeBall ();
		}
	}

	[Command]
	public void CmdCatchPokeBall () {
		info.isCatchingByPlayer = true;

		GameObject player = NetworkServer.FindLocalObject (info.touchingPlayerId);
		Transform playerTransform = player.transform;
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		gameObject.transform.position = playerTransform.position + playerTransform.forward + playerTransform.up;
		gameObject.transform.parent = player.transform;
	}
}
