using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthDeer : NetworkBehaviour {
		[SyncVar] public float health = 100;

		void Update(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (health <= 0) {
				enabled = false;
				NetworkServer.Destroy(transform.parent.gameObject);
			}

			//print (health);
		}

		void OnCollisionEnter(Collision col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (col.gameObject.name == "Arrow(Clone)") {
				NetworkServer.Destroy(col.gameObject);
				health -= col.relativeVelocity.magnitude/3;
			}
		}

		public void FallDamage(float airDist){
			if (airDist < 40) {
				health -= airDist * 1.5f;
			} else {
				health = 0;
			}
		}
	}
}
