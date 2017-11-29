using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BowScript : MonoBehaviour {
	//fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public float power = 4f;
	public float coolDown = 1f;
	public Texture2D crosshair;
	public bool _____________;
	
	//fields set dynamically
	public Vector3 launchPos;
	public Quaternion launchRot;
	public GameObject projectile;
	public float timeHeld = 0f;
	public bool isDrawn = false;
	public bool canShoot = false;
	public float nextShot = 0;
	Transform launchPointTrans;

	void OnGUI()
	{
		if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;
		
		float xMin = (Screen.width / 2) - 22;
		float yMin = (Screen.height / 2) - 20;
		GUI.DrawTexture(new Rect(xMin, yMin, 50, 50), crosshair);
	}

	void LateUpdate(){
		if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		if (Time.time >= nextShot)
			canShoot = true;

		if (GameObject.Find ("HunterCamera").GetComponent<HunterCameraController> ().paused)
			canShoot = false;

		launchPointTrans = transform.Find ("Middle");
		launchPos = launchPointTrans.position;
		launchRot = launchPointTrans.rotation;

		if(canShoot){		
			if (Input.GetMouseButtonUp (0)) {
				if (isDrawn) {
					isDrawn = false;
					nextShot = Time.time + coolDown;
					canShoot = false;
					projectile.GetComponent<Rigidbody> ().isKinematic = false;
					projectile.GetComponent<Rigidbody> ().velocity = projectile.transform.forward * power * timeHeld;
					timeHeld = 0;
				}
			}
			
			if (Input.GetMouseButtonDown (0)) {
				isDrawn = true;
				projectile = Instantiate (prefabProjectile) as GameObject;
				projectile.transform.position = launchPos;
				projectile.transform.rotation = launchRot;
				projectile.GetComponent<Rigidbody> ().isKinematic = true;
				NetworkServer.Spawn (projectile);
			}
	  		
			if (Input.GetMouseButton (0)) {
				if (isDrawn) {
					if (timeHeld < 6)
						timeHeld += 0.25f;
					
					projectile.transform.position = launchPos;
					projectile.transform.rotation = launchRot;
					//print (timeHeld);
				}
			}
		}
	}
}
