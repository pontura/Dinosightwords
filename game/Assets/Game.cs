﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
        
    private states lastState;
    public states state;
    
    public enum states
    {
        PAUSED,
        IDLE,
        ENDED
    }
    
    static Game mInstance = null;

    public static Game Instance
    {
        get
        {
            if (mInstance == null)
            {
               // Debug.LogError("Algo llama a Game antes de inicializarse");
            }
            return mInstance;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
        }
    }
	void Awake () {
        mInstance = this;

    }
    void Start  ()
    {
       GetComponent<CharacterManager>().Init();
    }

    void OnGamePaused(bool paused)
    {
        if (paused)
        {
            lastState = state;
            Time.timeScale = 0;
            state = states.PAUSED;
        }
        else
        {
            state = lastState;
            Time.timeScale = 1;
        }
    }
}
