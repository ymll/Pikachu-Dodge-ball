#pragma strict
function Start () {

}

function Update () {
}

function OnCollisionStay (col : Collision)
{
    if(col.gameObject.name == "Pokeball"){
	    if (Input.GetKeyDown ("e"||"E")){
	    	col.rigidbody.isKinematic = true;
	    	col.transform.position = gameObject.transform.position + gameObject.transform.forward;
			col.transform.parent = gameObject.transform;
			}
	}
}