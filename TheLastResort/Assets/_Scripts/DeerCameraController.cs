using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeerCameraController : MonoBehaviour {

    public GameObject player;
	public GameObject focus;
	public float rotateSpeed = 3;
	public bool paused = false;
	public float pauseDelay = 0.25f;

	private float pitch = 0.0f;
	private float nextPause;
	private bool canPause = true;
    private Vector3 camOffset;

    void Start()
    {
		UnPause();
        camOffset = transform.position - focus.transform.position;
		//camOffset = new Vector3(1,1,1);
    }

    void LateUpdate()
    {	
		if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		if (Time.time >= nextPause)
			canPause = true;

		if (!paused) {
			if (Input.GetKey (KeyCode.Escape) && canPause)
				Pause ();
			
			float horiz = Input.GetAxis ("Mouse X") * rotateSpeed;
			player.transform.Rotate (0, horiz, 0);

			float camAngle = player.transform.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler (0, camAngle, 0);
			
			if (pitch - rotateSpeed * Input.GetAxis ("Mouse Y") < 75 && pitch - rotateSpeed * Input.GetAxis ("Mouse Y") > -75)
				pitch -= rotateSpeed * Input.GetAxis ("Mouse Y");

			transform.eulerAngles = new Vector3 (pitch, camAngle, 0.0f);

			transform.position = focus.transform.position + (rotation * camOffset);
		} else {
			if (Input.GetKey (KeyCode.Escape) && canPause)
				UnPause();
		}
    }

	public void Pause(){
		paused = true;
		canPause = false;
		nextPause = Time.time + pauseDelay;
		Cursor.lockState = CursorLockMode.None;
		GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD> ().enabled = true;
	}

	public void UnPause(){
		paused = false;
		canPause = false;
		nextPause = Time.time + pauseDelay;
		Cursor.lockState = CursorLockMode.Locked;
		GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD> ().enabled = false;
	}
}
