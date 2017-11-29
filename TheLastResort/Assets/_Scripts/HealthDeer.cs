using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthDeer : NetworkBehaviour {
		[SyncVar] public float health = 100;
		private float damageCooldown = 0;
		[SyncVar] private float deer; 

		void Start(){
			deer = GameObject.Find ("NetworkManager").GetComponent<NetworkManagerOverrides> ().deer;
		}

		void Update(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (health <= 0) {
				enabled = false;
				deer--;
				NetworkServer.Destroy(transform.parent.gameObject);
			}

			if (deer == 0) {
				EndScene stop = FindObjectOfType<EndScene>(); 
				stop.EndGame();
			}
		}

		void OnCollisionEnter(Collision col){
			//if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			//	return;

			if (Time.time > damageCooldown) {
				if (col.gameObject.name == "Arrow(Clone)") {
					//print ((col.gameObject.GetComponent<Arrow> ().timeHeld * GameObject.Find ("Bow").GetComponent<BowScript> ().power) / (9 - col.gameObject.GetComponent<Arrow> ().timeHeld));
					health -= (col.gameObject.GetComponent<Arrow> ().timeHeld * GameObject.Find ("Bow").GetComponent<BowScript> ().power) / (9 - col.gameObject.GetComponent<Arrow> ().timeHeld);
					print (health);
					damageCooldown = Time.time + 0.1f;
				}
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
