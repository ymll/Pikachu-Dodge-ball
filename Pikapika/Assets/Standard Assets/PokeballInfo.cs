using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PokeballInfo : NetworkBehaviour {
	 
	[SyncVar]
	public bool isCatchingByPlayer;

	[SyncVar]
	public float lastPokeballCatchTime = Time.time;

	[SyncVar]
	public float lastPokeballThrownTime = Time.time;

	private Dictionary<NetworkInstanceId, bool> canCatchMap;

	// Use this for initialization
	void Start () {
		canCatchMap = new Dictionary<NetworkInstanceId, bool> ();
	}

	[Server]
	public bool canCatch (NetworkInstanceId netId) {
		bool value;
		return canCatchMap.ContainsKey (netId) && canCatchMap.TryGetValue(netId, out value) && value;
	}

	[Server]
	public void setCatch (NetworkInstanceId netId, bool canCatch) {
		canCatchMap.Remove (netId);
		canCatchMap.Add (netId, canCatch);
	}
}
