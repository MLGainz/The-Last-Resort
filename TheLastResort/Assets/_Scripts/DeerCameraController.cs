using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeerCameraController : MonoBehaviour {

    public GameObject player;
	public GameObject focus;
	public float rotateSpeed = 3;

	private float pitch = 0.0f;
	private float oldPitch;
    private Vector3 camOffset;

    void Start()
    {
        camOffset = transform.position - focus.transform.position;
		//camOffset = new Vector3(1,1,1);
    }

    void LateUpdate()
    {	
		if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;
		
		float horiz = Input.GetAxis("Mouse X") * rotateSpeed;
		player.transform.Rotate(0, horiz, 0);

		float camAngle = player.transform.eulerAngles.y;
		pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
		Quaternion rotation = Quaternion.Euler(0, camAngle, 0);
		
		if(pitch < 75 && pitch > -75){
			transform.eulerAngles = new Vector3(pitch, camAngle, 0.0f);

			oldPitch = pitch;
		}else{
			transform.eulerAngles = new Vector3(oldPitch, camAngle, 0.0f);
		}

		transform.position = focus.transform.position + (rotation*camOffset);
    }
}
