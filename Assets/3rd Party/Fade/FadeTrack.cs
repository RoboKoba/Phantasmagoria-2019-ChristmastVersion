using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Timeline.Sample {
    
    [System.Serializable]
    [TrackMediaType(TimelineAsset.MediaType.Script)]
    [TrackClipType(typeof(FadeAsset))]
    [TrackBindingType(typeof(CanvasGroup))]
    [TrackColor(0.2f, 1f, 0)]
    public class FadeTrack : TrackAsset {
                
        public Playable CreatePlayable(PlayableGraph graph, GameObject go) {
            
            var mixer = ScriptPlayable<FadePlayableBehaviour>.Create(graph);

            var content = go.GetComponent<PlayableDirector>();
            var canvasGroup = content.GetGenericBinding(this) as CanvasGroup;
//            var fadeAsset = clip.asset as FadeAsset;
            mixer.GetBehaviour().canvasGroup = canvasGroup;
//            mixer.GetBehaviour().fadeType = fadeAsset.fadeType;

//            clip.displayName = fadeAsset.fadeType.ToString();
            return mixer;
        }
        
    }
}