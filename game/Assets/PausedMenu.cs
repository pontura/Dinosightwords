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
    public void SoundsToogle()
    {
        if (Data.Instance.soundsVolume == 1)
            Data.Instance.soundsVolume = 0;
        else
            Data.Instance.soundsVolume = 1;

        Events.OnSoundsVolumeChanged(Data.Instance.soundsVolume);
    }
    public void MusicToogle()
    {
        if (Data.Instance.musicVolume == 1)
            Data.Instance.musicVolume = 0;
        else
            Data.Instance.musicVolume = 1;

        Events.OnMusicVolumeChanged(Data.Instance.musicVolume);
    }
}
