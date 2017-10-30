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
		/**Get the current mouse poition in 2D screen coordinates
		Vector3 mousePos2D = Input.mousePosition;
		//Convert the mouse position to 3D world coordinates
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
		//Find the delta from the launchPos to the mousePos3D
		Vector3 mouseDelta = mousePos3D-launchPos;
		//Limit mouseDelta to the radius of the Bow SphereCollider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		if(mouseDelta.magnitude > maxMagnitude){
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}
		//Move the projectile to this new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;
		*/
		if(Input.GetMouseButtonUp(0)){
			print("Bow:MouseButtonUp");
			timeHeld = 0;
		}
		
		if(Input.GetMouseButtonDown(0)){
			projectile = Instantiate(prefabProjectile) as GameObject;
			projectile.transform.position = launchPos;
		}
		
		if(Input.GetMouseButton(0)){
			if(timeHeld < 6)
				timeHeld += 0.1f;
			print(timeHeld);
		}
	}
	
	void Awake(){
		//Set the Bow singleton ScreenToWorldPoint
		S = this;
		
		Transform launchPointTrans = transform.Find("Middle");
		launchPoint = launchPointTrans.gameObject;
		launchPos = launchPointTrans.position;
	}
}
