using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


[Serializable]
public class PlayerInfo {

	//public static PlayerInfo instance; 
	public bool inGame {get; set;}
	public string playerName { get; set;}
	public Vector3 playerPos { get; set;}

}