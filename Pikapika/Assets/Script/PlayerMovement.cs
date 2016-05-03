using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// Use this for initialization
	void Start () {
		this.transform.position = GameObject.Find ("Network Manager").transform.position;
		GameObject mainCam = GameObject.Find ("Main Camera");
		mainCam.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
