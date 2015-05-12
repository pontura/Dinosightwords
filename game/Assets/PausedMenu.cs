﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PausedMenu : MonoBehaviour {

    [SerializeField] GameObject canvas;
    public GameObject soundsConfirmationCanvas;
    public Text soundsConfirmationText;

    public GameObject soundsOff;
    public GameObject musicOff;
    public GameObject capsOff;


    public void Init()
    {
        canvas.SetActive(true);
        soundsConfirmationCanvas.SetActive(false);

        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;
        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnCapsChanged += OnCapsChanged;

        OnSoundsVolumeChanged(Data.Instance.soundsVolume);
        OnMusicVolumeChanged(Data.Instance.musicVolume);
        OnCapsChanged(Data.Instance.caps);
	}
    void OnDestroy()
    {
        Events.OnSoundsVolumeChanged -= OnSoundsVolumeChanged;
        Events.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        Events.OnCapsChanged -= OnCapsChanged;
    }
    void OnCapsChanged(bool caps)
    {
        if (!capsOff) return;

        if (caps) capsOff.SetActive(false);
        else capsOff.SetActive(true);
    }
    void OnSoundsVolumeChanged(float vol)
    {
        if (!soundsOff) return;
        if (vol == 1)
            soundsOff.SetActive(false);
        else
            soundsOff.SetActive(true);
    }
    void OnMusicVolumeChanged(float vol)
    {
        if (!musicOff) return;
        if (vol == 1)
            musicOff.SetActive(false);
        else
            musicOff.SetActive(true);       
    }
    public void Restart()
    {
        Events.OnSoundFX("buttonPress");
        canvas.SetActive(false);
        GetComponent<ConfirmExit>().Init("Restart");
    }
    public void LevelSelector()
    {
        Events.OnSoundFX("buttonPress");
        canvas.SetActive(false);
        GetComponent<ConfirmExit>().Init("03_LevelSelector");
    }
    public void Close()
    {
        Events.OnSoundFX("backPress");
        Events.OnGamePaused(false);
        Game.Instance.state = Game.states.PAUSED;
        Invoke("Unpause", 0.3f);
        canvas.SetActive(false);
    }
    void Unpause()
    {
        Game.Instance.state = Game.states.PLAYING;
        Events.OnGamePaused(false);
    }
    public void SoundsToogle()
    {
        Events.OnSoundFX("buttonPress");
        print("SoundsToogle" + Data.Instance.soundsVolume);
        if (Data.Instance.soundsVolume == 1)
        {
            Events.OnSoundFX("warningPopUp");
            soundsConfirmationCanvas.SetActive(true);
            soundsConfirmationText.text = Data.Instance.GetComponent<TextsData>().SoundsOffConfirmation;
           
        }
        else
        {
            Data.Instance.soundsVolume = 1;
            Events.OnSoundsVolumeChanged(Data.Instance.soundsVolume);
        }
    }
    public void CapsToogle()
    {
        Events.OnSoundFX("buttonPress");
        print("CapsToogle" + Data.Instance.caps);
        if (Data.Instance.caps)
            Events.OnCapsChanged(false);
        else
            Events.OnCapsChanged(true);
    }
    public void CloseSoundsConfirmation()
    {
        Events.OnSoundFX("buttonPress");
        soundsConfirmationCanvas.SetActive(false);
    }
    public void SoundsOff()
    {
        Events.OnSoundFX("buttonPress");
        print("SoundsOff" + Data.Instance.soundsVolume);
        Events.OnSoundsVolumeChanged(0);
        CloseSoundsConfirmation();
    }
    public void MusicToogle()
    {
        Events.OnSoundFX("buttonPress");
        if (Data.Instance.musicVolume == 1)
            Data.Instance.musicVolume = 0;
        else
            Data.Instance.musicVolume = 1;

        Events.OnMusicVolumeChanged(Data.Instance.musicVolume);
    }
}
