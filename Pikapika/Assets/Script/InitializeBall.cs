using UnityEngine;
using System.Collections;

public class InitializeBall : MonoBehaviour {

	public GameObject ballMark;
	GameObject mark;

	void Start () {
		mark = Instantiate (ballMark, new Vector3(this.transform.position.x,this.transform.position.y+21,this.transform.position.z+1),Quaternion.identity) as GameObject;
		mark.name = "BallMark" ;
	}

	// Update is called once per frame
	void Update () {

	}
}
