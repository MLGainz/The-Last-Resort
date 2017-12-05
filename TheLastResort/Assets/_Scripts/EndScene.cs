using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {
	private NetworkManagerOverrides lobby;
	public float endGameDelay = 5;
	private float endGame = 0;
	private Timer timer;

	void Start(){
		lobby = GameObject.Find("NetworkManager").GetComponent<NetworkManagerOverrides> ();
		timer = FindObjectOfType<Timer> ();
	}

	void Update(){
		if (endGame != 0)
			if (Time.time >= endGame)
				EndGame ();
	}

	public void EndGame(){
		if(timer.timeLeft <= 295){
			if (endGame != 0) {
				//GameObject Hunter = GameObject.Find("Hunter(Clone)");
				//GameObject [] Deer = GameObject.FindGameObjectsWithTag("Deer");

				//for (int i = 0; i < Deer.Length; i++)
				//	Deer [i].SetActive(false);
				
				//Hunter.SetActive(false);

				Cursor.lockState = CursorLockMode.None;

				endGame = 0;
				lobby.deer = 0;
				lobby.isHunter = false;
				lobby.SendReturnToLobby ();
			} else {
				endGame = Time.time + endGameDelay;
			}
		}
	}
}
