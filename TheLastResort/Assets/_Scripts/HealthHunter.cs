﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthHunter : MonoBehaviour {
		public float health = 100;

		private bool canHit = true;
		private float hitAgain;

		void Update(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (Time.time > hitAgain)
				canHit = true;

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

		public void FallDamage(float airDist){
			if (airDist < 40) {
				health -= airDist * 1.5f;
			} else {
				health = 0;
			}
		}
	}
}
