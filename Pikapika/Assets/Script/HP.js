#pragma strict

public class HP extends NetworkBehaviour {
	@SyncVar
	public var HP: int = 3;
	private var panel: GameObject;
	private var image: GameObject[] ;
	private var die: GameObject[] ;

	function Start () {
		if (!isLocalPlayer) {
			return;
		}

		image = GameObject.FindGameObjectsWithTag("ball");
		die = GameObject.FindGameObjectsWithTag("die");
	}

	function Update () {
		if (!isLocalPlayer) {
			return;
		}

		updateUI (HP);
	}

	function beingHit(){
		if (!isServer) {
			return;
		}

		HP = HP - 1;
		resetPos ();
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

	function updateUI(HP : int){
		for (var i : int = 0; i < 3; i++) {
			var visibility = (HP > (2 - i));
			image[i].GetComponent.<UnityEngine.UI.Image>().enabled = visibility;
		}

		if (HP == 0) {
			die[0].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
			die[1].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
			//Destroy(gameObject);
			yield WaitForSeconds(1);
			die[0].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;
			die[1].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;

			GetComponent.<GhostMode>().EnterGhostMode(gameObject);
		}
	}
}
