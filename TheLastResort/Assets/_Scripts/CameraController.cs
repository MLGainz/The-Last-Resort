using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	public GameObject focus;
	public GameObject bow;
	public float rotateSpeed = 5;

	private float pitch = 0.0f;
	private float oldPitch;
	private float bowY = 0;
    private Vector3 camOffset;
	private Vector3 bowOffset;
	private Vector3 origBowOffset;

    void Start()
    {
        camOffset = transform.position - focus.transform.position;
		bowOffset = transform.position - bow.transform.position;
		origBowOffset = bowOffset;
		//camOffset = new Vector3(1,1,1);
    }

    void LateUpdate()
    {	
		float horiz = Input.GetAxis("Mouse X") * rotateSpeed;
		player.transform.Rotate(0, horiz, 0);

		float camAngle = bow.transform.eulerAngles.y;
		float bowAngle = player.transform.eulerAngles.y;
		pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
		Quaternion rotation = Quaternion.Euler(0, camAngle, 0);
		
		if(pitch < 75 && pitch > -75){
			bow.transform.eulerAngles = new Vector3(pitch, bowAngle, 0.0f);
			transform.eulerAngles = new Vector3(pitch, camAngle, 0.0f);
			/**
			if (pitch > 0 && pitch > oldPitch) {
				bowOffset.z -= pitch * 0.001f;
				print ("1");
			} else if (pitch > 0 && pitch < oldPitch) {
				bowOffset.z += pitch * 0.001f;
				print ("2");
			} else if (pitch < 0 && pitch > oldPitch) {
				bowOffset.z -= pitch * 0.001f;
				print ("3");
			} else if (pitch < 0 && pitch < oldPitch) {
				bowOffset.z += pitch * 0.001f;
				print ("4");
			} else if (pitch == 0) {
				bowOffset.z = origBowOffset.z;
			}
			*/

			oldPitch = pitch;
			bowY = -pitch * 0.02f;
		}else{
			bow.transform.eulerAngles = new Vector3(oldPitch, bowAngle, 0.0f);
			transform.eulerAngles = new Vector3(oldPitch, camAngle, 0.0f);
		}

		transform.position = focus.transform.position + (rotation*camOffset);
		bow.transform.position = new Vector3(transform.position.x, transform.position.y + bowY, transform.position.z) + (rotation*bowOffset);
    }
}
