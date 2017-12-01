using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HunterCameraController : MonoBehaviour {

    public GameObject player;
	public GameObject focus;
	public GameObject bow;
	public float rotateSpeed = 5;
	public bool paused = false;
	public float pauseDelay = 0.25f;
    //public Text MainText;
    //public Text EText;
    public Button MainButton;
    public Button EButton;
    public Canvas PCanvas;


	private float pitch = 0.0f;
	private float bowY = 0;
	private float nextPause;
	private bool canPause = true;
    private Vector3 camOffset;
	private Vector3 bowOffset;
	private Vector3 origBowOffset;

    void Start()
    {	
		UnPause();
        camOffset = transform.position - focus.transform.position;
		bowOffset = transform.position - bow.transform.position;
		origBowOffset = bowOffset;

        //MainButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        //EButton = GameObject.Find("ExitButton").GetComponent<Button>();
        MainButton = MainButton.GetComponent<Button>();
        EButton = EButton.GetComponent<Button>();
        PCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();

        //MainText = GameObject.Find("MainMenuText").GetComponent<Text>();
        //EText = GameObject.Find("ExitText").GetComponent<Text>();
        MainButton.enabled = false;
        EButton.enabled = false;
        PCanvas.enabled = false;

        //MainText.enabled = false;
        //EText.enabled = false;
    }

    void LateUpdate()
    {	
		if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer)
			return;
		
		if (Time.time >= nextPause)
			canPause = true;
		
		if (!paused) {
			if (Input.GetKey (KeyCode.Escape) && canPause)
				Pause();

			float horiz = Input.GetAxis ("Mouse X") * rotateSpeed;
			player.transform.Rotate (0, horiz, 0);

			float camAngle = bow.transform.eulerAngles.y;
			float bowAngle = player.transform.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler (0, camAngle, 0);

			if (pitch - rotateSpeed * Input.GetAxis ("Mouse Y") < 75 && pitch - rotateSpeed * Input.GetAxis ("Mouse Y") > -75)
				pitch -= rotateSpeed * Input.GetAxis ("Mouse Y");
			
			bow.transform.eulerAngles = new Vector3 (pitch, bowAngle, 0.0f);
			transform.eulerAngles = new Vector3 (pitch, camAngle, 0.0f);

			bowY = -pitch * 0.02f;

			transform.position = focus.transform.position + (rotation * camOffset);
			bow.transform.position = new Vector3 (transform.position.x, transform.position.y + bowY, transform.position.z) + (rotation * bowOffset);
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


        MainButton.enabled = true;
        EButton.enabled = true;
        PCanvas.enabled = true;

        //MainText.enabled = true;
        //EText.enabled = true;
	}

	public void UnPause(){
		paused = false;
		canPause = false;
		nextPause = Time.time + pauseDelay;
		Cursor.lockState = CursorLockMode.Locked;
		GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD> ().enabled = false;

        MainButton.enabled = false;
        EButton.enabled = false;
        PCanvas.enabled = false;

        //MainText.enabled = false;
        //EText.enabled = false;
	}
}
