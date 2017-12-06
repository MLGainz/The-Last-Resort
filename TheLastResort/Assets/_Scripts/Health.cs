using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	[SyncVar] public float hp;
	// Use this for initialization
	void Start () {
		hp = 100;
	}
}
