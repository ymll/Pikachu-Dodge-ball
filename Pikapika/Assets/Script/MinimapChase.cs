using UnityEngine;
using System.Collections;

public class MinimapChase : MonoBehaviour {
	string playerNum;
	// Use this for initialization
	void Start () {
		playerNum = this.name.Substring(0,1);
		Debug.Log (playerNum);
		this.transform.rotation = Quaternion.Euler(90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find (playerNum) != null)
			this.transform.position = new Vector3 (GameInfo.playerList [int.Parse (playerNum) - 1].playerPos.x, GameInfo.playerList [int.Parse (playerNum) - 1].playerPos.y + 20, GameInfo.playerList [int.Parse (playerNum) - 1].playerPos.z);
		else
			Destroy (this);
		//this.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y+20, this.transform.parent.position.z+1);
	}
}
