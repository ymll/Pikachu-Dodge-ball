using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


[Serializable]
public class GameInfo {

	public static GameInfo instance; 
	public static int numOfPlayer = 0;	
	public static List<PlayerInfo> playerList = new List<PlayerInfo> ();

}