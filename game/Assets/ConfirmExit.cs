using UnityEngine;
using System.Collections;

public class ConfirmExit : MonoBehaviour {

    public GameObject canvas;
    private string next;
    void Start()
    {
        canvas.SetActive(false);
    }
    public void Init(string next)
    {
        this.next = next;
        canvas.SetActive(true);
    }
    public void Close()
    {
        canvas.SetActive(false);
        Events.OnGamePaused(false);
    }
    public void Restart()
    {
        Close();
        
        if (next == "03_LevelSelector")
        {
            Time.timeScale = 1;
            Application.LoadLevel("03_LevelSelector");
        }
        else
        {
            Application.LoadLevel("04_Game");
            Events.OnGameRestart();
        }

    }
}
