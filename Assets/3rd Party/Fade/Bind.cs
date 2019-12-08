using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;

// bind canvas group on runtime.

public class Bind : MonoBehaviour {
	
	[SerializeField] CanvasGroup canvasGroup;
	void Start () {
		var director = GetComponent<PlayableDirector>();
		var fadeTrack = director.playableAsset.outputs.First (c => c.streamName=="Fade Track");
		director.SetGenericBinding(fadeTrack.sourceObject, canvasGroup);
		director.Play();
	}

}