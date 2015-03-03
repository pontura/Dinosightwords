using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


    public AudioClip minigameTile;


	void Start () {
        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;
        OnSoundsVolumeChanged(Data.Instance.soundsVolume);
	}
    void OnDestroy()
    {
        Events.OnSoundsVolumeChanged -= OnSoundsVolumeChanged;
    }
    void OnAudioButtonClicked()
    {
       // playSound(sfx_buttonClick, false);
    }
    public void OnPlaySoundFX(string audioName)
    {
       // if (audioName == "sfx_photo") playSound(sfx_photo, false);        
    }
    void OnSoundsVolumeChanged(float value)
    {
        audio.volume = value;
    }
    private void playSound(AudioClip _clip, bool looped = true)
    {

        audio.clip = _clip;
        audio.Play();
        audio.loop = looped;
    }
}
