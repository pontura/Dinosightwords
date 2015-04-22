using UnityEngine;
using System.Collections;

public class PausedMenu : MonoBehaviour {

    public GameObject canvas;

    public void Init()
    {
        canvas.SetActive(true);
	}

    public void Restart()
    {
        canvas.SetActive(false);
        GetComponent<ConfirmExit>().Init("Restart");
    }
    public void LevelSelector()
    {
        canvas.SetActive(false);
        GetComponent<ConfirmExit>().Init("03_LevelSelector");
    }
    public void Close()
    {
        Events.OnGamePaused(false);
        canvas.SetActive(false);
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
