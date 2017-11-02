using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	public GameObject focus;
	public GameObject bow;
	public float rotateSpeed = 5;
	
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private float oldPitch;
    private Vector3 camOffset;
	private Vector3 bowOffset;

    void Start()
    {
        camOffset = transform.position - player.transform.position;
		bowOffset = player.transform.position - bow.transform.position;
    }

    void LateUpdate()
    {	
		float horiz = Input.GetAxis("Mouse X") * rotateSpeed;
		float vert = Input.GetAxis("Mouse Y") * rotateSpeed;
		player.transform.Rotate(0, horiz, 0);
		
		float angle = player.transform.eulerAngles.y;
		pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
		
		if(pitch < 90 && pitch > -90){
			bow.transform.eulerAngles = new Vector3(pitch, angle, 0.0f);
			transform.eulerAngles = new Vector3(pitch, angle, 0.0f);
			oldPitch = pitch;
		}else{
			bow.transform.eulerAngles = new Vector3(oldPitch, angle, 0.0f);
			transform.eulerAngles = new Vector3(oldPitch, angle, 0.0f);
		}
		
		Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+3, player.transform.position.z) + (rotation*camOffset);
		bow.transform.position = player.transform.position + (rotation*bowOffset);
    }
}
