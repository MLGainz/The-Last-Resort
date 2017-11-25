using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerOverrides : NetworkLobbyManager {

	[SerializeField] GameObject m_PlayerPrefab1;
	[SerializeField] GameObject m_PlayerPrefab2;
	public bool isHunter = false;

	void Awake(){
		ClientScene.RegisterPrefab(m_PlayerPrefab1);
		ClientScene.RegisterPrefab(m_PlayerPrefab2);
	}

	public override GameObject OnLobbyServerCreateGamePlayer (NetworkConnection conn, short playerControllerId){
		GameObject player;
		Transform startPos = GetStartPosition();
		if (startPos != null)
		{	
			if (isHunter == false) {
				player = (GameObject)Instantiate (m_PlayerPrefab1, startPos.position, startPos.rotation);
				isHunter = true;
			} else {
				player = (GameObject)Instantiate (m_PlayerPrefab2, startPos.position, startPos.rotation);
			}
		}
		else
		{
			if (isHunter == false) {
				player = (GameObject)Instantiate(m_PlayerPrefab1, Vector3.zero, Quaternion.identity);
				isHunter = true;
			} else {
				player = (GameObject)Instantiate (m_PlayerPrefab2, Vector3.zero, Quaternion.identity);
			}
		}

		return(player);
	}
}
