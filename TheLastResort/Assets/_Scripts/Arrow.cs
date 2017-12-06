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
				}
			} else {
				this.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
	}
}