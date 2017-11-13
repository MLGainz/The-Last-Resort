using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthBarDeer : MonoBehaviour {
		Vector2 pos = new Vector2 (20, Screen.height - 100);
		Vector2 size = new Vector2(200,20); 
		public Texture2D emptyTex;
		public Texture2D fullTex;
		float health;

		void OnGUI(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;
			
			GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
				GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
				GUI.BeginGroup (new Rect (0, 0, health*2, size.y));
					GUI.Box (new Rect (0, 0, size.x, size.y), fullTex);
				GUI.EndGroup ();
			GUI.EndGroup ();
		}

		void Update(){
			HealthDeer script = GetComponent<HealthDeer> ();
			health = script.health;
		}
	}
}