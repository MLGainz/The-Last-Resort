using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHead : MonoBehaviour {
	static readonly int[] triangle_indices = { 0, 1, 2 };
	static readonly int[] line_indices = { 0, 1, 1, 2, 2, 0 };
	static readonly Vector3[] vertices = {
		new Vector3 (-10, -10, 0),
		new Vector3 (0, 10, 0),
		new Vector3 (10, -10, 0),
	};

	void make_triangle() {
			GameObject go = new GameObject ();
			go.name = "triangle";
			go.transform.localPosition = new Vector3 (-10, 0, 20);
			
			MeshFilter mf = go.AddComponent<MeshFilter> ();
			go.AddComponent<MeshRenderer> ();
			
			Mesh mesh = new Mesh ();
			mesh.vertices = vertices;
			mesh.triangles = triangle_indices;
			mf.mesh = mesh;
	}
}
