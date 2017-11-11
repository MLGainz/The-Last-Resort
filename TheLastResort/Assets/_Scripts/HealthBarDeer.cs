using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class HealthBarDeer : NetworkBehaviour {
		Vector2 pos = new Vector2 (20, Screen.height - 20);
		Vector2 size = new Vector2(60,20); 
		public Texture2D emptyTex;
		public Texture2D fullTex;
		[SyncVar] public float health = 100;

		void OnGUI(){
			GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
				GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
				GUI.BeginGroup (new Rect (0, 0, size.x * health, size.y));
					GUI.Box (new Rect (0, 0, size.x, size.y), fullTex);
				GUI.EndGroup ();
			GUI.EndGroup ();
		}

		void Update(){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
				return;

			if (health <= 0)
				Network.Destroy(this.gameObject);
		}

		void OnCollisionEnter(Collision col){
			if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isServer)
				return;

			if (col.gameObject.name == "Arrow(Clone)") {
				Destroy (col.gameObject);
				health -= col.relativeVelocity.magnitude/4;
				print (health);
			}
		}
	}
}
