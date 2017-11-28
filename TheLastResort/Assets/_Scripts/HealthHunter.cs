using System.Collections;
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
				EndScene stop = FindObjectOfType<EndScene>(); 
				stop.EndGame();
			}
		}

		void OnCollisionEnter(Collision col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			//print (col.gameObject.name);

			if (col.gameObject.name == "DeerBody") {
				Vector3 move = col.gameObject.GetComponent<DeerUserController> ().m_Move;
				float speed = Mathf.Abs(move.x) + Mathf.Abs(move.z);
				health -= speed*5;
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
