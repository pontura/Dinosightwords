﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
        
    private states lastState;
    public states state;
    public UI ui;
    
    public enum states
    {
        PAUSED,
        PLAYING,
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
	void Awake () {
        mInstance = this;

    }
    void Start  ()
    {
        Events.OnMusicChange("inGame");

        Events.OnGamePaused += OnGamePaused;
        Events.OnLevelComplete += OnLevelComplete;
        Events.StartGame += StartGame;
        Events.OnGameOver += OnGameOver;
        Events.OnGameRestart += OnGameRestart;

        GetComponent<GameManager>().Init();
        GetComponent<WordsManager>().Init();
        ui.Init();
        OnGamePaused(false);
    }
    void OnDestroy()
    {
        Events.OnGamePaused -= OnGamePaused;
        Events.StartGame -= StartGame;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnGameOver -= OnGameOver;
        Events.OnGameRestart -= OnGameRestart;
    }
    void StartGame()
    {
        state = states.PLAYING;
    }
    void OnLevelComplete()
    {
        state = states.PAUSED;
    }
    void OnGameOver()
    {
        state = states.PAUSED;       
    }
    
    void OnGameRestart()
    {
        Data.Instance.LoadLevel("04_Game", 1, 1, Color.black); 
        OnGamePaused(false);
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
