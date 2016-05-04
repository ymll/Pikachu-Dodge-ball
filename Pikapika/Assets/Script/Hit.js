#pragma strict
var HP : HP;
var lastHitTime: float;
var coolDown: int;
function Start () {
	lastHitTime = Time.time;
}

function Update () {

}

 function OnCollisionEnter (hit : Collision)
 {
		if((hit.transform.gameObject.name == "Pokeball")&&((Time.time - lastHitTime)>coolDown))
		{
		   //do stuff
		   HP.beingHit();
		   lastHitTime = Time.time;
		}
 }