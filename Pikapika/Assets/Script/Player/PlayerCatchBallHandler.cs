using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCatchBallHandler : NetworkBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		if (isLocalPlayer && Input.GetKey (KeyCode.E)) {
			NetworkInstanceId netId = GetComponent<NetworkIdentity> ().netId;
			CmdCatchPokeball (netId);
		}
	}

	[Command]
	void CmdCatchPokeball (NetworkInstanceId netId) {
		GameObject pokeball = GameObject.FindGameObjectWithTag("PlayPokeball");
		if (pokeball == null) {
			return;
		}

		PokeballInfo info = pokeball.GetComponent<PokeballInfo> ();

		// Ignore if ball has been caught or command comes from player not touching the ball
		if (info.isCatchingByPlayer || !info.canCatch(netId)) {
			return;
		}

		info.isCatchingByPlayer = true;

		Transform playerTransform = gameObject.transform;
		pokeball.GetComponent<Rigidbody>().isKinematic = true;
		pokeball.transform.position = playerTransform.position + playerTransform.forward + playerTransform.up;
		pokeball.transform.parent = gameObject.transform;

		info.lastPokeballCatchTime = Time.time;
	}
}

