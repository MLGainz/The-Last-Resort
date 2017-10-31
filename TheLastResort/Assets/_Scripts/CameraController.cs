using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	
	public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
	private float oldpitch;
	
    private Vector3 offset;
	private Vector3 lookPos;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {	
        transform.position = player.transform.forward + offset*-1;
		yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
		
		if(pitch < 90 && pitch > -90){
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
			player.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
			oldpitch = pitch;
		}else{
			transform.eulerAngles = new Vector3(oldpitch, yaw, 0.0f);
		}
    }
}
