using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballHitHandler : NetworkBehaviour {

	[SyncVar (hook="OnCanHurtPlayerChanged")]
	private bool canHurtPlayer;

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
		} else if (collider.CompareTag("Player")) {
			netId = collider.gameObject.GetComponent<NetworkIdentity> ().netId;
			return collider.gameObject;
		} else {
			netId = NetworkInstanceId.Invalid;
			return null;
		}
	}

	[ServerCallback]
	void Update () {
		if (!canHurtPlayer && info.isCatchingByPlayer) {
			canHurtPlayer = true;
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
		if (!canHurtPlayer) {
			return;
		}

		float currentTime = Time.time;
		NetworkInstanceId netId = NetworkInstanceId.Invalid;
		GameObject player = null;

		// Find which player is hit
		foreach (ContactPoint contact in collision.contacts) {
			player = getCollisionGameObject (contact.otherCollider, out netId);
			if (netId != NetworkInstanceId.Invalid) {
				Debug.Log ("Hit player " + netId);
				break;
			}
		}

		// If no body hit, pokeball can be touched without losing life
		if (netId == NetworkInstanceId.Invalid) {
			canHurtPlayer = false;
		} else {
			player.SendMessage ("beingHit");
		}
	}

	private void OnCanHurtPlayerChanged(bool canHurtPlayer) {
		GameObject ballMark = GameObject.Find ("BallMark");

		if (canHurtPlayer) {
			ballMark.GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			ballMark.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
