using UnityEngine;
using System.Collections;

public class ConfirmExit : MonoBehaviour {

    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
    }
    public void Init()
    {
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
        Application.LoadLevel("04_Game");
        Events.OnGameRestart();
    }
}
