using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Arrow : NetworkBehaviour {
	[SyncVar] public float timeHeld;
	private bool isFired = false;
	private Vector3 empty = new Vector3(0.0f, 0.0f, 0.0f);

	void Start(){
		timeHeld = 0;
	}

	// Update is called once per frame
	void LateUpdate(){
		if (this.GetComponent<Rigidbody> ().velocity == empty && !isFired) {
			timeHeld = GameObject.Find ("Bow").GetComponent<BowScript> ().timeHeld;
		} else if (this.GetComponent<Rigidbody> ().velocity != empty) {
			isFired = true;
		} else if (this.GetComponent<Rigidbody> ().velocity == empty && isFired) {
			timeHeld = 0;
		}
	}

	void OnCollisionEnter(Collision col){
		
	}
}
