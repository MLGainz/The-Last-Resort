using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour {
	public int minutes = 5;
	[SyncVar] public string winner;
	public Text timer;
	public Text winText;

	private int seconds;
	[SyncVar] public float timeLeft;

	void Start(){
		winText.text = "";
		winner = "";
		seconds = 0;
		timeLeft = minutes * 60;
	}

	// Update is called once per frame
	void LateUpdate () {
		winText.text = winner;
		if (timeLeft > 0) {
			timer.text = minutes.ToString () + ":" + ((int)seconds / 10).ToString () + "" + ((int)seconds % 10).ToString ();
			timeLeft -= Time.deltaTime;
			minutes = (int)(timeLeft / 60);
			seconds = (int)(timeLeft % 60);
		} else {
			winner = "The Deer Win";
			EndScene stop = FindObjectOfType<EndScene>(); 
			stop.EndGame();
		}
	}
}
