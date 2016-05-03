#pragma strict
private var HP: int = 3;
var image0: GameObject;
var image1: GameObject;
var image2: GameObject;

function Start () {

}

function Update () {
	if (Input.GetKeyDown("space")){
    	HP = HP - 1;
    	print(HP);
    	image2.gameObject.SetActive(false);
    	updateUI(HP);
    }

}

function updateUI(HP){
	switch(HP){
		case 2:
			image2.gameObject.SetActive(false);
			break;
		case 1:
			image1.gameObject.SetActive(false);
			break;
		case 0:
			image0.gameObject.SetActive(false);
			break;
	}
}