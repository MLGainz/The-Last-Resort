﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerOverrides : NetworkLobbyManager {

	[SerializeField] GameObject m_PlayerPrefab1;
	[SerializeField] GameObject m_PlayerPrefab2;
	public bool isHunter = false;
	public int deer = 0;

	void Awake(){
		ClientScene.RegisterPrefab(m_PlayerPrefab1);
		ClientScene.RegisterPrefab(m_PlayerPrefab2);
	}

	public override void OnClientSceneChanged (NetworkConnection conn)
	{
		string loadedSceneName = networkSceneName;
		if (loadedSceneName == lobbyScene) {
			if (client.isConnected)
				CallOnClientEnterLobby ();
		} else {
			CallOnClientExitLobby ();
		}

		//base.OnClientSceneChanged (conn);
		ClientScene.Ready(conn);
		OnLobbyClientSceneChanged(conn);
	}


	void CallOnClientEnterLobby(){
		deer = 0;
		isHunter = false;

		OnLobbyClientEnter ();
		foreach (var player in lobbySlots) {
			if (player == null)
				continue;

			player.readyToBegin = false;
			player.OnClientEnterLobby ();
		}
	}

	void CallOnClientExitLobby(){
		OnLobbyClientExit ();
		foreach (var player in lobbySlots) {
			if (player == null)
				continue;

			player.readyToBegin = false;
			player.OnClientExitLobby ();
		}
	}

	public override GameObject OnLobbyServerCreateGamePlayer (NetworkConnection conn, short playerControllerId){
		GameObject player = new GameObject();
		Transform startPos = GetStartPosition();

		if (isHunter == false) {
			int hunt = Random.Range (1, 4-deer);
			if (hunt == 1 || numPlayers - 1 == deer ) {
				player = (GameObject)Instantiate (m_PlayerPrefab1, startPos.position, startPos.rotation);
				isHunter = true;
			} else {
				player = (GameObject)Instantiate (m_PlayerPrefab2, startPos.position, startPos.rotation);
				deer += 1;
			}
		} else {
			player = (GameObject)Instantiate (m_PlayerPrefab2, startPos.position, startPos.rotation);
		}

		return(player);
	}
}
