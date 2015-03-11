using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
        
    private states lastState;
    public states state;
    public UI ui;
    
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
	void Awake () {
        mInstance = this;

    }
    void Start  ()
    {
        GetComponent<GameManager>().Init();
        GetComponent<WordsManager>().Init();
        ui.Init();
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
