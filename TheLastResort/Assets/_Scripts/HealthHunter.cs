﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthHunter : NetworkBehaviour {
		[SyncVar] public float health = 100;

		void Update(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (health <= 0) {
				enabled = false;
				NetworkServer.Destroy(transform.parent.gameObject);
			}
		}

		void OnCollisionEnter(Collision col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			print (col.gameObject.name);

			if (col.gameObject.name == "DeerBody") {
				health -= col.relativeVelocity.magnitude/2;
			}
		}
	}
}
