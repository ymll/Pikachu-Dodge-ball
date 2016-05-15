#pragma strict
import UnityEngine.Networking;

//var HP : HP;

public class Hit extends NetworkBehaviour{
	var lastHitTime: float;
	var coolDown: int;
	function Start () {
		lastHitTime = Time.time;
	}

	function Update () {

	}

	 function OnCollisionEnter (hit : Collision){
	 		print(hit.gameObject.name);
			if((hit.transform.gameObject.name == "Pokeball(Clone)")&&((Time.time - lastHitTime)>coolDown))
			{
			   //do stuff
			   transform.GetComponent.<HP>().beingHit();
			   //HP.beingHit();
			   lastHitTime = Time.time;
			}
	 }
 }