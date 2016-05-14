using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokeballInfo : NetworkBehaviour {

	[SyncVar]
	public bool isCatchingByPlayer;

	public GameObject touchingPlayer;

	public float lastPokeballCatchTime = Time.time;

	public float lastPokeballThrownTime = Time.time;
}
