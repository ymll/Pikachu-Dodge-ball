using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EndGame : NetworkBehaviour {
	private GameObject[] win;
	public GameObject[] playerList;
	public bool onceStarted;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}
		playerList = GameObject.FindGameObjectsWithTag("Player");
		if (playerList.Length>=2)
			onceStarted = true;
		if ((playerList.Length == 1)&&(onceStarted)) {
			win = GameObject.FindGameObjectsWithTag("win");
			win[0].gameObject.GetComponent<CanvasGroup>().alpha = 1f;
		}
	}
}
