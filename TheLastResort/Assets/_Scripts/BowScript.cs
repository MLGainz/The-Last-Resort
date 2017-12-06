using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class BowScript : NetworkBehaviour {
		//fields set in the Unity Inspector pane
		public GameObject propArrow;
		public GameObject prefabProjectile;
		public GameObject prefabFakeProjectile;
		public float power = 4f;
		public float coolDown = 1f;
		public Texture2D crosshairNormal;
		public Texture2D crosshairHit;
		public bool _____________;

		//fields set dynamically
		public Vector3 launchPos;
		public Quaternion launchRot;
		[SyncVar]public float timeHeld = 0f;
		public bool isDrawn = false;
		public bool canShoot = false;
		[SyncVar]public bool deerHit = false;
		public float nextShot = 0;
		public float hitMarkerTime;
		Transform launchPointTrans;

		private Texture2D crosshair;
		private GameObject fakeProjectile;
		private AudioSource audio;

		void Start(){
			crosshair = crosshairNormal;
			audio = this.GetComponent<AudioSource> ();
		}

		void OnGUI()
		{
			if (!gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			float xMin = (Screen.width / 2) - 22;
			float yMin = (Screen.height / 2) - 20;
			GUI.DrawTexture (new Rect (xMin, yMin, 50, 50), crosshair);
		}

		void LateUpdate(){
			if (!gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (deerHit && crosshair != crosshairHit) {
				crosshair = crosshairHit;
				hitMarkerTime = Time.time + 0.25f;
			}

			if (!deerHit && crosshair != crosshairNormal) {
				crosshair = crosshairNormal;
			}

			if (Time.time >= hitMarkerTime)
				deerHit = false;

			if (Time.time >= nextShot)
				canShoot = true;

			if (GameObject.Find ("HunterCamera").GetComponent<HunterCameraController> ().paused)
				canShoot = false;

			launchPointTrans = GameObject.Find("Bow").transform.Find ("arrowStart");
			launchPos = launchPointTrans.position;
			launchRot = launchPointTrans.rotation;

			if(canShoot){	
				if (Input.GetMouseButtonDown (0)) {
					isDrawn = true;
					propArrow.SetActive (true);

				}

				if (Input.GetMouseButtonUp (0)) {
					if (isDrawn) {
						audio.Play ();
						isDrawn = false;
						nextShot = Time.time + coolDown;
						canShoot = false;
						propArrow.SetActive (false);
						fakeProjectile = Instantiate (prefabFakeProjectile, launchPos, launchRot);
						fakeProjectile.GetComponent<Rigidbody> ().isKinematic = false;
						fakeProjectile.GetComponent<Rigidbody> ().velocity = fakeProjectile.transform.forward * power * timeHeld;
						CmdShootArrow (launchPos, launchRot, power, timeHeld);
						timeHeld = 0;
					}
				}

				if (Input.GetMouseButton (0)) {
					if (isDrawn) {
						if (timeHeld < 6)
							timeHeld += 0.25f;
						//print (launchRot);
						propArrow.transform.position = launchPos - (launchRot * ((timeHeld/8)*new Vector3(0,0,1)));
						propArrow.transform.rotation = launchRot;
					}
				}
			}
		}

		[Command]
		public void CmdShootArrow(Vector3 pos, Quaternion rot, float pow, float tHeld){
			GameObject projectile = Instantiate (prefabProjectile, pos, rot);
			projectile.GetComponent<Rigidbody> ().isKinematic = false;
			projectile.GetComponent<Rigidbody> ().velocity = projectile.transform.forward * pow * tHeld;
			projectile.GetComponent<Arrow> ().timeHeld = tHeld;
			NetworkServer.Spawn (projectile);


		}
	}
}

