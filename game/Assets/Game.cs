using UnityEngine;
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
        GetComponent<GameManager>().Init();
        GetComponent<WordsManager>().Init();
        ui.Init();
    }
    void Destroy()
    {
        Events.OnGamePaused -= OnGamePaused;
        Events.StartGame -= StartGame;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnGameOver -= OnGameOver;
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
        OnGamePaused(true);
        Invoke("Restart", 2);  
    }
    void Restart()
    {
        Application.LoadLevel("04_Game");
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
