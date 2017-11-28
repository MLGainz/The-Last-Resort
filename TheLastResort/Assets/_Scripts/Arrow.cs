using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public float timeHeld;

	void Start(){
		timeHeld = 0;
	}

	// Update is called once per frame
	void LateUpdate(){
		if (this.GetComponent<Rigidbody> ().velocity == new Vector3 (0.0f, 0.0f, 0.0f)) {
			timeHeld = GameObject.Find ("Bow").GetComponent<BowScript> ().timeHeld;
		}
	}

	void OnCollisionEnter(Collision col){
		
	}
}
