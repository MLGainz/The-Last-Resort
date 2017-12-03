using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Arrow : NetworkBehaviour {
	[SyncVar] public float timeHeld;

	void LateUpdate(){
		if (this.GetComponent<Rigidbody> ().velocity == new Vector3 (0, 0, 0)) {
			timeHeld = 0;
		}
	}
}
