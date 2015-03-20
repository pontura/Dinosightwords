using UnityEngine;
using System.Collections;

public class PausedMenu : MonoBehaviour {

	void Start () {
	
	}

    public void Restart()
    {
        Time.timeScale = 1;
        Application.LoadLevel("04_Game");
        Events.OnGameRestart();
    }
    public void LevelSelector()
    {
        Time.timeScale = 1;
        Application.LoadLevel("03_LevelSelector");
    }
    public void Close()
    {
        Events.OnGamePaused(false);
        gameObject.SetActive(false);
    }
    public void Sounds()
    {
    }
    public void Musics()
    {
    }
}
