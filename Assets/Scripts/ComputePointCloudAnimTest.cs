using UnityEngine;


public class ComputePointCloudAnimTest : MonoBehaviour {
	
	[Range(-2f,2f)]
	public float Param1Scale = 1f;
	[Range(-2f,2f)]
	public float Param2Scale = 1f;
	[Range(-2f,2f)]
	public float Param3Scale = 1f;
	[Range(0.01f,22f)]
	public float Param1Frequency = 1f;
	[Range(0.01f,22f)]
	public float Param2Frequency = 1f;
	[Range(0.01f,22f)]
	public float Param3Frequency = 1f;
	
	private PointAnimation animController;

	// Use this for initialization
	void Start () {
		animController = gameObject.GetComponent<PointAnimation>();
		Debug.Log(animController);
	}
	
	// Update is called once per frame
	void Update () {
		animController._param1 = (Mathf.Sin(Mathf.PerlinNoise(0f, Time.time/Param1Frequency)) * Param1Scale) + Param1Scale;
		animController._param2 = (Mathf.Cos(Mathf.PerlinNoise(0f, Time.time/Param2Frequency)) * Param2Scale) + Param2Scale;
		animController._param3 = (Mathf.Sin(Mathf.PerlinNoise(0f, Time.time/Param3Frequency)) * Param3Scale) + Param3Scale;
	}
}
