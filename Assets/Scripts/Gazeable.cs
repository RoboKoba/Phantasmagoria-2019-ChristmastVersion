using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazeable : MonoBehaviour
{

	public GazeManager CameraGazeManager;
	private bool IsFadingIn = true;  // Assume we start faded in already
	private bool IsFadingOut;

	void Start()
	{
		CameraGazeManager = Camera.main.gameObject.GetComponent<GazeManager>();
	}
	
	void Update()
	{
		if (CameraGazeManager.GazedAt == gameObject && !IsFadingIn)
		{
			gameObject.GetComponent<SimpleAnimation>().Play("fadein");
			IsFadingIn = true;
			IsFadingOut = false;
		}
		else if (CameraGazeManager.GazedAt != gameObject && !IsFadingOut)
		{
			gameObject.GetComponent<SimpleAnimation>().Play("fadeout");
			IsFadingIn = false;
			IsFadingOut = true;
		}
		
	}

	public void FadedOut()
	{
		IsFadingIn = false;
		IsFadingOut = false;
	}	
	
	public void FadedIn()
	{
		IsFadingIn = false;
		IsFadingOut = false;
	}	
	
}
