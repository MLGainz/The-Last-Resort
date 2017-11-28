using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class Minimap : MonoBehaviour {
		public GameObject player;
		public GameObject playerArrowPrefab;
		public GameObject enemyDotPrefab;
		public float heightOffset;
		public Camera cam;

		private GameObject playerArrow;
		private GameObject enemyDot;
		private Vector3 moving; 
		private Vector3 empty = new Vector3(0,0,0);

		void Start(){
			playerArrow = Instantiate (playerArrowPrefab) as GameObject;
			enemyDot = Instantiate (enemyDotPrefab) as GameObject;

			if (playerArrowPrefab.name == "Player_Arrow_Hunter") {
				cam.cullingMask &= ~(1 << 14);
			} else if (playerArrowPrefab.name == "Player_Arrow_Deer") {
				cam.cullingMask &= ~(1 << 13);
			}
		}

		// Update is called once per frame
		void LateUpdate () {
			playerArrow.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + heightOffset - 10, player.transform.position.z);
			playerArrow.transform.rotation = player.transform.rotation;

			enemyDot.transform.position = playerArrow.transform.position;
			enemyDot.transform.rotation = player.transform.rotation;

			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) 
				return;
			
			this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + heightOffset, player.transform.position.z);

			if (player.name == "HunterBody") {
				moving = player.GetComponent<HunterUserController>().m_Move;

				if (moving == empty) {
					enemyDot.SetActive(false);
				} else {
					enemyDot.SetActive (true);
				}
			} else if (player.name == "DeerBody") {
				moving = player.GetComponent<DeerUserController>().m_Move;

				if (moving == empty) {
					enemyDot.SetActive (false);
				} else {
					enemyDot.SetActive (true);
				}
			}
		}
	}
}
