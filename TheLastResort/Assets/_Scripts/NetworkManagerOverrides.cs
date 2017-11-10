using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerOverrides : NetworkManager {

	[SerializeField] GameObject m_PlayerPrefab1;
	[SerializeField] GameObject m_PlayerPrefab2;
	public bool isHunter = false;

	void Awake(){
		ClientScene.RegisterPrefab(m_PlayerPrefab1);
		ClientScene.RegisterPrefab(m_PlayerPrefab2);
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader){
		OnServerAddPlayer(conn, playerControllerId);
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		if (m_PlayerPrefab1 == null || m_PlayerPrefab2 == null)
		{
			if (LogFilter.logError) { Debug.LogError("A PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object."); }
			return;
		}

		if (m_PlayerPrefab1.GetComponent<NetworkIdentity>() == null || m_PlayerPrefab2.GetComponent<NetworkIdentity>() == null)
		{
			if (LogFilter.logError) { Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab."); }
			return;
		}

		if (playerControllerId < conn.playerControllers.Count  && conn.playerControllers[playerControllerId].IsValid && conn.playerControllers[playerControllerId].gameObject != null)
		{
			if (LogFilter.logError) { Debug.LogError("There is already a player at that playerControllerId for this connections."); }
			return;
		}

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

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}
