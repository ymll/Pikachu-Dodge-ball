using UnityEngine;
using System.Collections;

public class BallMark : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.rotation = Quaternion.Euler(90, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Pokeball(Clone)") != null) {
			float x = GameObject.Find ("Pokeball(Clone)").transform.position.x;
			float y = GameObject.Find ("Pokeball(Clone)").transform.position.y + 21;
			float z = GameObject.Find ("Pokeball(Clone)").transform.position.z + 1;
			this.transform.position = new Vector3 (x, y, z);
		} else {
			Destroy (this);
		}
		//this.transform.position = new Vector3(GameInfo.playerList [int.Parse (playerNum) - 1].playerPos.x,GameInfo.playerList [int.Parse (playerNum) - 1].playerPos.y+20,GameInfo.playerList [int.Parse (playerNum) - 1].playerPos.z);
		//this.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y+20, this.transform.parent.position.z+1);
	}
}
