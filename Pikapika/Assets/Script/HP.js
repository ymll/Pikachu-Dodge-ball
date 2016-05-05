#pragma strict
private var HP: int = 3;
var panel: GameObject;
private var image: GameObject[] ;

function Start () {
     image = GameObject.FindGameObjectsWithTag("ball");
}

function Update () {
	if (Input.GetKeyDown("space")){

    }

}
function beingHit(){
    HP = HP - 1;
	//print(HP);
	updateUI(HP);
}

function updateUI(HP){
	switch(HP){
		case 2:
			image[0].gameObject.SetActive(false);
			break;
		case 1:
			image[1].gameObject.SetActive(false);
			break;
		case 0:
			image[2].gameObject.SetActive(false);
			break;
	}
}