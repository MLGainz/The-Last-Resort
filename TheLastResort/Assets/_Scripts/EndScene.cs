using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {
	private NetworkManagerOverrides lobby;

	void Start(){
		lobby = FindObjectOfType<NetworkManagerOverrides> ();
	}

	public void EndGame(){
		GameObject Hunter = GameObject.Find("Hunter(Clone)");
		GameObject [] Deer = GameObject.FindGameObjectsWithTag("Deer");

		for (int i = 0; i < Deer.Length; i++)
			Deer [i].SetActive(false);
		
		Hunter.SetActive(false);

		Cursor.lockState = CursorLockMode.Confined;

		lobby.deer = 0;
		lobby.isHunter = false;
		lobby.SendReturnToLobby ();
	}
}
