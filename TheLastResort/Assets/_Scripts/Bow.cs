using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {
	static public Bow S;
	
	//fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public float power = 4f;
	
	//fields set dynamically
	public GameObject launchPoint;
	public Vector3 launchPos;
	public Vector3 vel;
	public Quaternion launchRot;
	public GameObject projectile;
	public float timeHeld = 0f;
	public Transform launchPointTrans;
	
	void Update(){		
		if(Input.GetMouseButtonUp(0)){
			launchPos = launchPointTrans.position;
			launchRot = launchPointTrans.rotation;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			print(launchRot.x);
			print(launchRot.y);
			print(launchRot.z);
			
			if(launchRot.y == 0){
				vel.x = power*timeHeld;
			}else{
				//vel.x = -launchRot.y*power*timeHeld;
				vel.z = -launchRot.z*power*timeHeld;
			}
			projectile.GetComponent<Rigidbody>().velocity = vel;

			timeHeld = 0;
		}
		
		if(Input.GetMouseButtonDown(0)){
			projectile = Instantiate(prefabProjectile) as GameObject;
			projectile.transform.position = launchPos;
			projectile.transform.rotation = launchRot;
			projectile.GetComponent<Rigidbody>().isKinematic = true;
		}
		
		if(Input.GetMouseButton(0)){
			if(timeHeld < 4){
				timeHeld += 0.5f;
			
			projectile.transform.position = launchPos;
			print(timeHeld);
		}
	}
	
	void Awake(){
		//Set the Bow singleton ScreenToWorldPoint
		S = this;
		
		launchPointTrans = transform.Find("Middle");
		launchPoint = launchPointTrans.gameObject;
		launchPos = launchPointTrans.position;
		launchRot = launchPointTrans.rotation;
	}
}
