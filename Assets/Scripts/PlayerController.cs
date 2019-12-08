using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR;


public class PlayerController : MonoBehaviour

{
	public Transform Player1Start;
	public Transform Player2Start;
	public float resetHeight = -0.75f;
	public bool IsPlayer1 = true;
	private Transform VRCamera;
	public GameObject timeline;
	
	private PlayableDirector playableDirector;
	private bool IsPlaying = false;


	void Start()
	{
		playableDirector = timeline.GetComponent<PlayableDirector>();
		VRCamera = Camera.main.transform;
		Calibrate();
		RewindAndPauseTimeline();
	}
	
	void Update()
	{
		//if (VRCamera.localPosition.y < resetHeight)
		if (XRDevice.userPresence == UserPresenceState.Present && !IsPlaying)
		{
            Calibrate();
            playableDirector.Play();
            IsPlaying = true;
		}
		if (XRDevice.userPresence != UserPresenceState.Present && IsPlaying)
		{
			RewindAndPauseTimeline();
            IsPlaying = false;
		}
		
		if (Input.GetButtonUp("Submit"))
		{
			Calibrate();
			if (IsPlaying)
			{
				RewindAndPauseTimeline();
                IsPlaying = false;
			}
			else
			{
				RewindAndPlayTimeline();
                IsPlaying = true;
			}
		}
	}

	public void Calibrate()
	{
		var playerStart = IsPlayer1 ? Player1Start.position : Player2Start.position;
		var position = gameObject.transform.position;
		position.x += playerStart.x - VRCamera.position.x;
		position.y += playerStart.y - VRCamera.position.y;
		position.z += playerStart.z - VRCamera.position.z;
		gameObject.transform.position = position;
	}

	public void RewindAndPlayTimeline()
	{
		timeline.SetActive(true);
		playableDirector.time = 0;
		playableDirector.Play();
	}
	
	public void RewindAndPauseTimeline()
	{
		playableDirector.time = 0;
		Invoke(nameof(DoPause), .1f);
	}

	public void DoPause()
	{
		playableDirector.Play();
		playableDirector.Pause();
	}
}