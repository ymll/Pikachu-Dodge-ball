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
	if (Input.GetKeyDown ("f"||"F")){
		beingHit();
	}

}
function beingHit(){
    HP = HP - 1;
	//print(HP);
	updateUI(HP);
	resetPos();
}

function resetPos(){
	var playerList : GameObject[];
	var numPlayer : int;
	playerList = GameObject.FindGameObjectsWithTag ("Player");
	numPlayer = playerList.Length;
	if (numPlayer>=1)
		playerList [0].transform.position = GameObject.Find ("1st player").transform.position;
	if (numPlayer>=2)
		playerList [1].transform.position = GameObject.Find ("2nd player").transform.position;
	if (numPlayer>=3)
		playerList [2].transform.position = GameObject.Find ("3rd player").transform.position;
	if (numPlayer>=4)
		playerList [3].transform.position = GameObject.Find ("4th player").transform.position;

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
			//Destroy(gameObject);
			yield WaitForSeconds(1);
			die[0].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;
			die[1].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;

			GetComponent.<GhostMode>().EnterGhostMode(gameObject);
			break;
	}
}