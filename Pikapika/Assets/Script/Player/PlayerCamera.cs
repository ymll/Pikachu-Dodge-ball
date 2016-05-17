using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCamera : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Camera>().enabled = isLocalPlayer;
	}
}
