using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthDeer : MonoBehaviour {
		public float health;
		public float damageCooldown = 0;

		public AudioClip ac_land;
		public AudioClip ac_water;
		public AudioClip ac_splash;
		private AudioSource audio; 
		private DeerUserController user;
		private FirstPersonDeer deer;
		private Timer timer;

		void Start(){
			audio = this.GetComponent<AudioSource> ();
			user = this.GetComponent<DeerUserController> ();
			deer = this.GetComponent<FirstPersonDeer> ();
			timer = FindObjectOfType<Timer> ();
		}

		void Update(){
			if (!deer.m_IsGrounded && audio.isPlaying)
				audio.Stop ();

			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			health = gameObject.transform.root.GetComponent<Health> ().hp;

			if (user.m_Move == new Vector3 (0, 0, 0) && audio.isPlaying)
				audio.Stop ();

			if (health <= 0) {
				enabled = false;
				GameObject.Find ("NetworkManager").GetComponent<NetworkManagerOverrides> ().deer--;
				NetworkServer.Destroy(transform.parent.gameObject);
			}

			if (GameObject.Find ("NetworkManager").GetComponent<NetworkManagerOverrides> ().deer == 0) {
				EndScene stop = FindObjectOfType<EndScene>(); 
				Timer winner = FindObjectOfType<Timer> ();
				winner.winner = "The Hunter Wins";
				stop.EndGame();
			}
		}


		void OnCollisionEnter(Collision col){
			
		}

		void OnCollisionStay(Collision col){
			if (col.gameObject.name == "Terrain_PlayingField") {
				if (audio.isPlaying)
					return;

				audio.Play ();
			}
		}

		void OnTriggerEnter(Collider col){
			if (!deer.m_IsGrounded && col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				if (audio.isPlaying)
					audio.Stop ();

				audio.clip = ac_splash;
				audio.Play ();
			} else if (col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				if (audio.isPlaying)
					audio.Stop ();

				audio.clip = ac_water;
				audio.pitch = 1.5f;
				audio.Play ();
			}

			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (Time.time > damageCooldown) {
				if (col.gameObject.name == "Arrow(Clone)") {
					print ((col.gameObject.GetComponent<Arrow> ().timeHeld));// * GameObject.Find ("Hunter(Clone)").GetComponent<BowScript> ().power) / (9 - col.gameObject.GetComponent<Arrow> ().timeHeld));
					this.gameObject.transform.root.GetComponent<Health> ().hp = health - (col.gameObject.GetComponent<Arrow> ().timeHeld * GameObject.Find ("Hunter(Clone)").GetComponent<BowScript> ().power) / (9 - col.gameObject.GetComponent<Arrow> ().timeHeld);
					print (health);
					damageCooldown = Time.time + 1f;
				}
			}
		}

		void OnTriggerStay(Collider col){
			if (col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				//inWater = true;

				if (audio.isPlaying) {
					if (audio.clip == ac_land) {
						audio.Stop ();
					} else {
						return;
					}
				}
				audio.clip = ac_water;
				audio.Play ();
			}
		}

		void OnTriggerExit(Collider col){
			if (col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				audio.Stop ();
				audio.clip = ac_land;
				audio.pitch = 1;
			}
		}

		public void FallDamage(float airDist){
			if (airDist < 40) {
				this.gameObject.transform.root.GetComponent<Health> ().hp -= airDist * 1.5f;
			} else {
				this.gameObject.transform.root.GetComponent<Health> ().hp = 0;
			}
		}
	}
}