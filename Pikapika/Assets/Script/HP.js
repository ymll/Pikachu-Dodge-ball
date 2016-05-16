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
		updateUI (HP);
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
