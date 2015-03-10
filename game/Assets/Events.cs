using UnityEngine;
using System.Collections;

public static class Events {

    public static System.Action<GameObject> OnUIClicked = delegate { };

    public static System.Action<string> OnPreloadScene = delegate { };
    public static System.Action<string> OnSceneLoad = delegate { };
    public static System.Action OnLoadSceneReady = delegate { };
    public static System.Action OnSceneReset = delegate { };
    public static System.Action OnTransition = delegate { };
    public static System.Action OnTransitionReady = delegate { };

    public static System.Action<SwipeDetector.directions> OnSwipe = delegate { };

    public static System.Action<float, float> OnSaveVolumes = delegate { };
    public static System.Action<float> OnMusicVolumeChanged = delegate { };
    public static System.Action<float> OnSoundsVolumeChanged = delegate { };   
    public static System.Action OnGameComplete = delegate { };
    public static System.Action<bool> OnGamePaused = delegate { };


    public static System.Action OnHeroJump = delegate { };
    public static System.Action OnChangeingLane = delegate { };
    public static System.Action OnChangeLaneComplete = delegate { };

    public static System.Action<LaneObjectData> OnPlayerHitObject = delegate { };

    public static System.Action SetNextWord = delegate { };

}
