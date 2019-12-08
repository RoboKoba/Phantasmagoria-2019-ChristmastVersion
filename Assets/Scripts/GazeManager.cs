using UnityEngine;


public class GazeManager : MonoBehaviour {
	
	public float sightlength = 100.0f;
	public GameObject GazedAt;
	
	void Update()
	{
		RaycastHit seen;
		Ray raydirection = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(raydirection, out seen, sightlength))
		{
			var gazable = seen.collider.GetComponent<Gazeable>();
			GazedAt = gazable.gameObject;
		}
		else
		{
			GazedAt = null;
		}
	}
}