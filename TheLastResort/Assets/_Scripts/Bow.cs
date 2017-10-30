using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {
	static public Bow S;
	
	//fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _____________;
	
	//fields set dynamically
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public float timeHeld = 0f;
	
	void Update(){
		if(Input.GetMouseButtonUp(0)){
 +			launchPos = launchPointTrans.position;
 +			launchRot = launchPointTrans.rotation;
 +			projectile.GetComponent<Rigidbody>().isKinematic = false;
 +			print(launchRot.x);
 +			print(launchRot.y);
 +			print(launchRot.z);
 +			
 +			vel.x = power * timeHeld;
 
 +			projectile.GetComponent<Rigidbody>().velocity = vel;
 +
  			timeHeld = 0;
  		}
		
		if(Input.GetMouseButtonDown(0)){
  			projectile = Instantiate(prefabProjectile) as GameObject;
  			projectile.transform.position = launchPos;
 +			projectile.transform.rotation = launchRot;
 +			projectile.GetComponent<Rigidbody>().isKinematic = true;
  		}
  		
  		if(Input.GetMouseButton(0)){
 +			if(timeHeld < 4)
 +				timeHeld += 0.5f;
 +			
 +			projectile.transform.position = launchPos;
  			print(timeHeld);
  		}
	}
	
	void Awake(){
		S = this;
		
		launchPointTrans = transform.Find("Middle");
  		launchPoint = launchPointTrans.gameObject;
  		launchPos = launchPointTrans.position;
 +		launchRot = launchPointTrans.rotation;
	}
}
