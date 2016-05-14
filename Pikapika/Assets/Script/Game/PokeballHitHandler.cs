using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballHitHandler : NetworkBehaviour {

	public float minBallHoldingTime = 5;

	public float maxAttackTime = 5;

	private PokeballInfo info;

	// Use this for initialization
	void Start () {
		info = gameObject.GetComponent<PokeballInfo> ();
	}

	[ServerCallback]
	void OnCollisionEnter(Collision collision) {
		float currentTime = Time.time;

		if (info.touchingPlayerId.IsEmpty()) {
			foreach (ContactPoint contact in collision.contacts) {
				if (contact.otherCollider.CompareTag ("Pikachu")) {
					GameObject touchingPlayer = contact.otherCollider.transform.parent.gameObject;
					info.touchingPlayerId = touchingPlayer.GetComponent<NetworkIdentity> ().netId;
					Debug.Log ("Touched by a player ");
					break;
				}
			}	
		}

		// Player need to hold the ball for a while before throwing it.
		if (currentTime - info.lastPokeballCatchTime < minBallHoldingTime) {
			return;
		}

		// If no one hold Pokeball for a long time, no one will get hurt when touching it.
		if (currentTime - info.lastPokeballThrownTime >= maxAttackTime) {
			return;
		}

		handleHitPlayer ();
	}

	[ServerCallback]
	void OnCollisionExit(Collision collision) {
		if (!info.touchingPlayerId.IsEmpty()) {
			foreach (ContactPoint contact in collision.contacts) {
				GameObject contactObject = contact.otherCollider.gameObject;
				NetworkInstanceId touchingPlayerId = contactObject.GetComponent<NetworkIdentity>().netId;
				if (info.touchingPlayerId.Equals(touchingPlayerId)) {
					info.touchingPlayerId = new NetworkInstanceId ();
					return;
				}
			}
		}
	}

	private void handleHitPlayer() {
		if (info.touchingPlayerId.IsEmpty()) {
			return;
		}

		Debug.Log ("Hit!");

		// -HP
	}
}
