#pragma strict
private var HP: int = 3;
private var panel: GameObject;
private var image: GameObject[] ;
private var die: GameObject[] ;

function Start () {
     image = GameObject.FindGameObjectsWithTag("ball");
     die = GameObject.FindGameObjectsWithTag("die");
}

function Update () {

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
			die[0].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
			die[1].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
			die[2].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
			Destroy(gameObject);
			break;
	}
}