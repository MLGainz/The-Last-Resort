using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthHunter : MonoBehaviour {
		public float health = 100;
		public AudioClip ac_land;
		public AudioClip ac_water;
		public AudioClip ac_splash;

		private bool canHit = true;
		private float hitAgain;
		private AudioSource audio; 
		private HunterUserController user;
		private FirstPersonHunter hunter;
		private Timer timer;

		void Start(){
			audio = this.GetComponent<AudioSource> ();
			user = this.GetComponent<HunterUserController> ();
			hunter = this.GetComponent<FirstPersonHunter> ();
			timer = FindObjectOfType<Timer> ();
		}

		void Update(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			//if (user.m_Move == new Vector3 (0, 0, 0) && audio.isPlaying)
			//	audio.Stop ();

			if (Time.time > hitAgain)
				canHit = true;

			if (health <= 0) {
				EndScene stop = FindObjectOfType<EndScene>(); 
				Timer winner = FindObjectOfType<Timer> ();
				winner.winner = "The Deer Win";
				stop.EndGame();
			}

			//if (!hunter.m_IsGrounded && audio.isPlaying)
			//	audio.Stop ();
		}

		void OnCollisionEnter(Collision col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			//print (col.gameObject.name);

			if (col.gameObject.name == "DeerBody") {
				if (canHit) {
					if (col.gameObject.GetComponent<DeerUserController> ().charge) {
						health -= 15;
					} else {
						health -= 7.5f;
					}
					hitAgain = Time.time + 5;
					canHit = false;
				}
			}
		}
		/*
		void OnCollisionStay(Collision col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;
			
			if (col.gameObject.name == "Terrain_PlayingField") {
				if (audio.isPlaying)
					return;

				audio.Play ();

				if(user.sprint)
					audio.pitch = 1.5f;

				if (user.crouch)
					audio.volume = 0.5f;
			}
		}

		void OnTriggerEnter(Collider col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;
			
			if (!hunter.m_IsGrounded && col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				if (audio.isPlaying)
					audio.Stop ();

				audio.clip = ac_splash;
				audio.Play ();
			} else if (col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				if (audio.isPlaying)
					audio.Stop ();

				audio.clip = ac_water;
				audio.Play ();
			}
		}

		void OnTriggerStay(Collider col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;
			
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
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;
			
			if (col.gameObject.name == "Water_River" || col.gameObject.name == "Water_Waterfall") {
				audio.Stop ();
				audio.clip = ac_land;
			}
		}
		*/
		public void FallDamage(float airDist){
			if(timer.timeLeft <= 295)
				if (airDist < 40) {
					health -= airDist * 1.5f;
				} else {
					health = 0;
				}
		}
	}
}
