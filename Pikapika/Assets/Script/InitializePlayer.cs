using UnityEngine;
using System.Collections;

public class InitializePlayer : MonoBehaviour {
	// Use this for initialization
	//int minPlayerToStart = 4; // the min no. of player required to start a game
	public GameObject playerMark;
	GameObject mark;
	void Start () {
		
		GameInfo.numOfPlayer +=1;
		this.name = GameInfo.numOfPlayer.ToString (); 

		GameInfo.playerList.Add(new PlayerInfo() {playerName=this.name, playerPos=this.transform.position});
		mark = Instantiate (playerMark, new Vector3(this.transform.position.x,this.transform.position.y+20,this.transform.position.z+1),Quaternion.identity) as GameObject;
		mark.name = "M"+this.name ;
	}
	
	// Update is called once per frame
	void Update () {
		GameInfo.playerList [int.Parse(this.name)-1].playerPos = this.transform.position;
	}


}
