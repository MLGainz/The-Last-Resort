using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class Arrow : NetworkBehaviour {
		[SyncVar] public float timeHeld;
		private AudioSource audio;

		void Start(){
			audio = this.GetComponent<AudioSource> ();
		}

		void LateUpdate(){
			if (this.GetComponent<Rigidbody> ().velocity == new Vector3 (0, 0, 0)) {
				timeHeld = 0;
			}
		}

		void OnCollisionEnter(Collision col){
			if(timeHeld > 0)
				audio.Play ();
			
			if (col.gameObject.name == "DeerBody") {
				if (timeHeld != 0) {
					GameObject.Find ("Hunter(Clone)").GetComponent<BowScript> ().deerHit = true;
					//col.gameObject.transform.root.GetComponent<Health>().hp -= (timeHeld * GameObject.Find ("Hunter(Clone)").GetComponent<BowScript> ().power) / (9 - timeHeld);
					//col.gameObject.GetComponent<HealthDeer>().damageCooldown = Time.time + 1f;
					//print (col.gameObject.GetComponent<HealthDeer> ().health);
				}
			} else {
				this.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
	}
}