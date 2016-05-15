using UnityEngine;
using System.Collections;

public class InitializePlayer : MonoBehaviour {
	// Use this for initialization
	//int minPlayerToStart = 4; // the min no. of player required to start a game
	public GameObject playerMark;
	GameObject mark;
	void Start () {
		
		GameInfo.numOfPlayer +=1;
		Debug.Log(GameInfo.numOfPlayer);
		this.name = GameInfo.numOfPlayer.ToString (); 
		/*
		this.transform.position = GameObject.Find ("Network Manager").transform.position;

		if ((GameInfo.numOfPlayer%2 == 0)&&(GameInfo.numOfPlayer>=minPlayerToStart)){
			Debug.Log("Game start~");
			GameInfo.playerList.Add(new PlayerInfo() {playerName=this.name, playerPos=this.transform.position, inGame = true});
			GameInfo.playerList[int.Parse(this.name)-2].inGame = true;
		}else{
			Debug.Log("Needa wait for more players~~");
			GameInfo.playerList.Add(new PlayerInfo() {playerName=this.name, playerPos=this.transform.position, inGame = false});
		}
		*/
		//GameObject mainCam = GameObject.Find ("Main Camera");
		//mainCam.transform.parent = this.transform;
		GameInfo.playerList.Add(new PlayerInfo() {playerName=this.name, playerPos=this.transform.position});
		mark = Instantiate (playerMark, new Vector3(this.transform.position.x,this.transform.position.y+10,this.transform.position.z+1),Quaternion.identity) as GameObject;
		mark.name = this.name+"Mark" ;
	}
	
	// Update is called once per frame
	void Update () {
		GameInfo.playerList [int.Parse(this.name)-1].playerPos = this.transform.position;
	}
}
