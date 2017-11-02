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

    void Start()
    {
        camOffset = transform.position - focus.transform.position;
		bowOffset = transform.position - bow.transform.position;
		//camOffset = new Vector3(1,1,1);
    }

    void LateUpdate()
    {	
		float horiz = Input.GetAxis("Mouse X") * rotateSpeed;
		player.transform.Rotate(0, horiz, 0);
		
		float angle = player.transform.eulerAngles.y;
		pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
		
		if(pitch < 90 && pitch > -90){
			bow.transform.eulerAngles = new Vector3(pitch, angle, 0.0f);
			transform.eulerAngles = new Vector3(pitch, angle, 0.0f);
			oldPitch = pitch;
			bowY = -pitch * 0.03f;
		}else{
			bow.transform.eulerAngles = new Vector3(oldPitch, angle, 0.0f);
			transform.eulerAngles = new Vector3(oldPitch, angle, 0.0f);
		}

		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		transform.position = focus.transform.position + (rotation*camOffset);
		bow.transform.position = new Vector3(transform.position.x, transform.position.y + bowY, transform.position.z) + (rotation*bowOffset);
    }
}
