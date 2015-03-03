using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public Canvas panel;
    private string nextScene;
    AsyncOperation async;
    public Animation anim;
    public states state;

    public enum states
    {
        IDLE,
        LOADING,
        PRELOADING,
        PRELOADING_READY,
        READY_TO_LOAD,
        CLOSING
    }
    
    void Start()
    {
        Events.OnTransition += OnTransition;
        Events.OnPreloadScene += OnPreloadScene;
        Events.OnSceneLoad += OnSceneLoad;

        panel.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        //if (async != null)
        //    print(state + " " + async.progress);

        if (state == states.PRELOADING && async != null && async.progress >= 0.9f)
        {
            Events.OnLoadSceneReady();
            state = states.PRELOADING_READY;
        }
        if (state == states.READY_TO_LOAD)
        {            
            if (async != null && async.progress >=0.9f)
            {
                async.allowSceneActivation = true;
                CloseMask();
            }
        }
    }
    public void OnPreloadScene(string _nextScene)
    {
        async = Application.LoadLevelAsync(_nextScene);
        async.allowSceneActivation = false;
        state = states.PRELOADING;
    }
    void OnSceneLoad(string _nextScene)
    {
        Time.timeScale = 1;
        this.nextScene = _nextScene;
        panel.gameObject.SetActive(true);

        if (state != states.PRELOADING && state != states.PRELOADING_READY)
            state = states.LOADING;

        anim.Play("SceneTransitionOn");
        Invoke("animationReady", 1);
    }
    public void animationReady()
    {
        if (state == states.LOADING)
        {
            async = Application.LoadLevelAsync(nextScene);
            async.allowSceneActivation = false;
        }
        state = states.READY_TO_LOAD;
    }
    public void CloseMask()
    {
        state = states.CLOSING;
        anim.Play("SceneTransitionOff");
        async = null;
        Invoke("ClosedReady", 1);
    }
    public void ClosedReady()
    {
        panel.gameObject.SetActive(false);

        if(state == states.CLOSING)
            state = states.IDLE;
    }
    public void OnTransition()
    {
        panel.gameObject.SetActive(true);
        anim.Play("SceneTransitionOn");
        anim["SceneTransitionOn"].normalizedTime = 0;
        Invoke("TransitionReady", 1); 
    }
    public void TransitionReady()
    {
        Events.OnTransitionReady();
        anim.Play("SceneTransitionOff");
        anim["SceneTransitionOff"].normalizedTime = 0;
        Invoke("ClosedReady", 1);
    }

}
