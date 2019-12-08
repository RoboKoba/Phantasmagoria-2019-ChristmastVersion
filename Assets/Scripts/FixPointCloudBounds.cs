#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


[ExecuteInEditMode]
public class FixPointCloudBounds : MonoBehaviour {

	void Start ()
	{
		Mesh mesh;
		
		#if UNITY_EDITOR
			if (!EditorApplication.isPlaying)
			{
				MeshFilter mf = GetComponent<MeshFilter>();
				mesh = mf.sharedMesh;
			}
			else
			{
				mesh = GetComponent<MeshFilter>().mesh;
			}
		#else
			mesh = GetComponent<MeshFilter>().mesh;
		#endif

		mesh.bounds = new Bounds(mesh.bounds.center, mesh.bounds.size * 10);
	}
	
}
