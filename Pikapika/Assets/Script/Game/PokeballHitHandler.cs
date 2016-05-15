using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballHitHandler : NetworkBehaviour {

	public float maxAttackTime = 5;

	private PokeballInfo info;

	// Use this for initialization
	void Start () {
		info = gameObject.GetComponent<PokeballInfo> ();
	}

	private GameObject getCollisionGameObject (Collider collider, out NetworkInstanceId netId) {
		if (collider.CompareTag ("Pikachu")) {
			GameObject touchingPlayer = collider.transform.parent.gameObject;
			netId = touchingPlayer.GetComponent<NetworkIdentity> ().netId;
			return touchingPlayer;
		} else {
			netId = NetworkInstanceId.Invalid;
			return null;
		}
	}

	[ServerCallback]
	void OnTriggerEnter (Collider other) {
		NetworkInstanceId netId;
		getCollisionGameObject (other, out netId);

		if (netId != NetworkInstanceId.Invalid) {
			Debug.Log ("OnTriggerEnter: player " + netId);
			info.setCatch (netId, true);
		}
	}

	[ServerCallback]
	void OnTriggerExit (Collider other) {
		NetworkInstanceId netId;
		getCollisionGameObject (other, out netId);

		if (netId != NetworkInstanceId.Invalid) {
			Debug.Log ("OnTriggerExit: player " + netId);
			info.setCatch (netId, false);
		}
	}

	[ServerCallback]
	void OnCollisionEnter (Collision collision) {
		float currentTime = Time.time;
		NetworkInstanceId netId = NetworkInstanceId.Invalid;
		GameObject player = null;

		// If no one hold Pokeball for a long time, no one will get hurt when touching it.
		if (currentTime - info.lastPokeballThrownTime >= maxAttackTime) {
			return;
		}

		// Find which player is hit
		foreach (ContactPoint contact in collision.contacts) {
			player = getCollisionGameObject (contact.otherCollider, out netId);
			if (netId != NetworkInstanceId.Invalid) {
				Debug.Log ("Hit player " + netId);
				break;
			}
		}

		// Ignore if no body hit
		if (netId == NetworkInstanceId.Invalid) {
			return;
		}

		// Decrease HP
	}
}
