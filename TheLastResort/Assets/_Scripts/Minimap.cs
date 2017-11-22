using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Minimap : MonoBehaviour {
	public GameObject player;
	public float heightOffset;

	// Update is called once per frame
	void LateUpdate () {
		if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) 
			return;
		
		this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + heightOffset, player.transform.position.z);
	}
}
