using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameStarter : NetworkBehaviour {

	public bool isGameStarted;

	private int connectedPlayerCount;

	public GameObject pokeballPrefab;

	public Transform pokeballSpawn;

	private GameObject pokeball;

	private GameObject[] playerList;

	// Update is called once per frame
	void Update () {
		if (!isServer)
		{
			return;
		}

		playerList = GameObject.FindGameObjectsWithTag ("Player");
		int connectedPlayerCount = playerList.Length;

		if (isGameStarted) {
			if (connectedPlayerCount <= 1) {
				isGameStarted = false;
				DestroyPokeball ();
			}
		} else {
			if (connectedPlayerCount >= 2) {
				isGameStarted = true;
				SpawnNewPokeball ();
			}
		}

		if (!isGameStarted && connectedPlayerCount >= 2) {
			
		}
	}

	void OnGUI() {
		GUILayout.Label("Connections: " + playerList.Length);
	}

	void SpawnNewPokeball() {
		pokeball = (GameObject)Instantiate(
			pokeballPrefab,
			pokeballSpawn.position,
			pokeballSpawn.rotation);

		NetworkServer.Spawn(pokeball);
	}

	void DestroyPokeball() {
		NetworkServer.Destroy (pokeball);
	}
}
