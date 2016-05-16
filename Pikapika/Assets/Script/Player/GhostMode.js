#pragma strict
private var Player: GameObject;

public class GhostMode extends NetworkBehaviour{
	function Start () {

	}

	function Update () {

	}

	function EnterGhostMode(Player){
		GetComponent(HP).enabled = false;
		GetComponent(ballPosition).enabled = false;
		GetComponent(CharacterController).enabled = false;
		transform.localScale = new Vector3(0, 0, 0);
		(GetComponent("PlayerThrowBallHandler") as NetworkBehaviour).enabled = false;
		(GetComponent("PlayerCatchBallHandler") as NetworkBehaviour).enabled = false;
	}
}