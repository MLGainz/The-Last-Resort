using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {
	Vector2 pos = new Vector2 (Screen.width/20, Screen.height/20);
	public float size = 150;
	public float heightOffset = 30;
	public GameObject player;
	public Canvas canvas;
	public RawImage minimap;
	public RenderTexture mapTexture;

	// Use this for initialization
	void Start () {
		//canvas = Instantiate (canvas);
		//minimap = canvas.I
		//minimap.uvRect = new Rect (pos.x, pos.y, size, size);
		//minimap.texture = mapTexture;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + heightOffset, player.transform.position.z);
	}

	void OnGUI(){
		
	}
}
