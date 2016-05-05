#pragma strict
private var catched: boolean = false;
private var pokeball: GameObject;
function Start () {

}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		if (catched){
			print("FIRE");
			pokeball.GetComponent.<Rigidbody>().isKinematic = false;
			pokeball.transform.parent = null;
			pokeball.GetComponent.<Rigidbody>().AddForce(gameObject.transform.forward * 20, ForceMode.Impulse);

		}
	}
}

function OnCollisionStay (col : Collision)
{
    if(col.gameObject.name == "Pokeball"){
	    if (Input.GetKeyDown ("e"||"E")){
	    	pokeball = col.gameObject;
	    	col.rigidbody.isKinematic = true;
	    	col.transform.position = gameObject.transform.position + gameObject.transform.forward;
			col.transform.parent = gameObject.transform;
			catched = true;
		}
	}
}