using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballInfo : NetworkBehaviour {

	[SyncVar]
	public NetworkInstanceId pokeballId;

	[SyncVar]
	public bool isCatchingByPlayer;

	public NetworkInstanceId touchingPlayerId;

	[SyncVar]
	public float lastPokeballCatchTime = Time.time;

	[SyncVar]
	public float lastPokeballThrownTime = Time.time;

	// Use this for initialization
	void Start () {
		pokeballId = gameObject.GetComponent<NetworkIdentity> ().netId;
	}
}
