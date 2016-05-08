using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


[Serializable]
public class CourtInfo {

	public static CourtInfo instance; 
	public static Vector3[] courtDimension = 
		new[] {
			new Vector3(-12f,6f,10f),
			new Vector3(15f,6,10f),
			new Vector3(-12f,6f,-10f),
			new Vector3(15f,6f,-10f)
		};
}