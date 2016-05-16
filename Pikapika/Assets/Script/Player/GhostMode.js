#pragma strict
public var Ref: GameObject;
public class GhostMode extends NetworkBehaviour{
	function Start () {

	}

	function Update () {

	}

	function EnterGhostMode(Player: GameObject){
		if (!isLocalPlayer)
		{
			return;
		}

		GetComponent(HP).enabled = false;
		GetComponent(ballPosition).enabled = false;
		//GetComponent(CharacterController).enabled = false;
		//transform.localScale = new Vector3(0, 0, 0);
		(GetComponent("PlayerThrowBallHandler") as NetworkBehaviour).enabled = false;
		(GetComponent("PlayerCatchBallHandler") as NetworkBehaviour).enabled = false;
		//Network.RemoveRPCs(Player);
		//Network.Destroy(Player);
		Ref = Player;
		//CmdDestroy(Player);


		var child : GameObject;
		child = GameObject.Find(this.name+"/Pikachu");
		try{
			Destroy(child.GetComponent(Collider));
		//Destroy(child.GetComponent(SphereCollider));
		}catch(e){

		}


		//destroy mark on minimap
		var Mark : GameObject;
		Mark = GameObject.Find("M"+this.name);
		Destroy(Mark);

	}

	@Command
	public function CmdDestroy(Player: GameObject){
		//Destroy(Player);
	}
	public function FixedUpdate() {
		if (NetworkServer.active) {
			//Network.Destroy(Ref);
		}
	}
}