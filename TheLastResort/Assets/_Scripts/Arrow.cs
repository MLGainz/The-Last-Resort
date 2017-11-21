using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	// Update is called once per frame
	void OnCollisionEnter(Collision col){
		GetComponent<Rigidbody> ().isKinematic = true;
	}
}
