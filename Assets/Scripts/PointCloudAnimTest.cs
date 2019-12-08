using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudAnimTest : MonoBehaviour {

	private Material mat;

	// Use this for initialization
	void Start () {
		mat = gameObject.GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		//float size = mat.GetFloat("_PointSize");
		mat.SetFloat("_PointSize", Mathf.Sin(Mathf.PerlinNoise(0f, Time.time/10f))/100f);
	}
}
