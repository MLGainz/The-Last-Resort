using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BowScript : NetworkBehaviour {
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
		if (!gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;
		
		float xMin = (Screen.width / 2) - 22;
		float yMin = (Screen.height / 2) - 20;
		GUI.DrawTexture(new Rect(xMin, yMin, 50, 50), crosshair);
	}

	void LateUpdate(){
		if (!gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		if (Time.time >= nextShot)
			canShoot = true;

		if (GameObject.Find ("HunterCamera").GetComponent<HunterCameraController> ().paused)
			canShoot = false;

		launchPointTrans = GameObject.Find("Bow").transform.Find ("Middle");
		launchPos = launchPointTrans.position;
		launchRot = launchPointTrans.rotation;

		if(canShoot){	
			if (Input.GetMouseButtonDown (0)) {
				isDrawn = true;
				CmdMakeArrow (launchPos, launchRot);
			}

			if (Input.GetMouseButtonUp (0)) {
				if (isDrawn) {
					isDrawn = false;
					nextShot = Time.time + coolDown;
					canShoot = false;
					CmdShootArrow (power, timeHeld);
					timeHeld = 0;
				}
			}
	  		
			if (Input.GetMouseButton (0)) {
				if (isDrawn) {
					if (timeHeld < 6)
						timeHeld += 0.25f;
					
					CmdMoveArrow (launchPos, launchRot);
					//print (timeHeld);
				}
			}
		}
	}

	[Command]
	public void CmdMakeArrow(Vector3 Pos, Quaternion Rot){
		projectile = Instantiate (prefabProjectile, Pos, Rot) as GameObject;
		projectile.GetComponent<Rigidbody> ().isKinematic = true;
		NetworkServer.Spawn (projectile);
	}

	[Command]
	public void CmdMoveArrow(Vector3 Pos, Quaternion Rot){
		projectile.transform.position = launchPos;
		projectile.transform.rotation = launchRot;
	}

	[Command]
	public void CmdShootArrow(float pow, float tHeld){
		projectile.GetComponent<Rigidbody> ().isKinematic = false;
		projectile.GetComponent<Rigidbody> ().velocity = projectile.transform.forward * pow * tHeld;
	}
}

