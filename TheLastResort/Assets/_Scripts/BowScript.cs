using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {
	//fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public float power = 4f;
	public bool _____________;
	
	//fields set dynamically
	public Vector3 launchPos;
	public Quaternion launchRot;
	public GameObject projectile;
	public float timeHeld = 0f;
	public bool isDrawn = false;
	Transform launchPointTrans;
	
	void LateUpdate(){
		launchPointTrans = transform.Find("Middle");
		launchPos = launchPointTrans.position;
 		launchRot = launchPointTrans.rotation;
			
		if(Input.GetMouseButtonUp(0)){
			isDrawn = false;
 			projectile.GetComponent<Rigidbody>().isKinematic = false;
 			projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * power * timeHeld;
  			timeHeld = 0;
  		}
		
		if(Input.GetMouseButtonDown(0)){
			isDrawn = true;
  			projectile = Instantiate(prefabProjectile) as GameObject;
  			projectile.transform.position = launchPos;
 			projectile.transform.rotation = launchRot;
 			projectile.GetComponent<Rigidbody>().isKinematic = true;
  		}
  		
  		if(Input.GetMouseButton(0)){
 			if(timeHeld < 6)
 				timeHeld += 0.25f;
			
			projectile.transform.position = launchPos;
			projectile.transform.rotation = launchRot;
  			print(timeHeld);
  		}
	}
}
