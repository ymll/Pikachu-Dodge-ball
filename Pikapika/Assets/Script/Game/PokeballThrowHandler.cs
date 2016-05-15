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
}
