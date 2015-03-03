using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip InterfacesTheme;
    public AudioClip MainTheme;
    public AudioClip MinigameTheme;
    public AudioClip SummaryTheme;

    private float volume;

    private float heartsDelay = 0.1f;

	// Use this for initialization
	public void Init () {
        audio.loop = true;
        OnMusicVolumeChanged(Data.Instance.musicVolume);

        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
	}
    void OnSoundsFadeTo(float to)
    {
        if (to > 0) to = volume;
       // TweenVolume tv = TweenVolume.Begin(gameObject, 1, to);
        //tv.PlayForward();
        //tv.onFinished.Clear();
    }
    private void playSound(AudioClip _clip, bool looped = true)
    {        
        if (audio.clip && audio.clip.name == _clip.name) return;
        stopAllSounds();
        audio.volume = volume;
        audio.clip = _clip;
        audio.Play();
        audio.loop = looped;
    }
    void OnMusicVolumeChanged(float value)
    {
        audio.volume = value;
        volume = value;
    }
    void OnGamePaused(bool paused)
    {
        print("OnGamePaused");
        if(paused)
            audio.Stop();
        else
            audio.Play();
    }
    void OnInterfacesStart()
    {
        playSound(InterfacesTheme);
    }
    void OnBadgeSelected(int totalPhotos)
    {
        OnBoardGame();
    }
    void OnBoardGame()
    {
        playSound(MainTheme);
    }
    void OnMinigameOpen(int playerID, int id)
    {
        playSound(MinigameTheme);
	}
    void OnSummary()
    {
        playSound(SummaryTheme);
        audio.loop = false;
    }
    void OnMain()
    {
        audio.loop = true;
        OnInterfacesStart();
    }
   
   
    void stopAllSounds()
    {
        audio.Stop();
    }

    float nextHeartSoundTime;
    public void addHeartSound()
    {
        if (Time.time >= nextHeartSoundTime)
        {
         // audio.PlayOneShot(heartClip);
          nextHeartSoundTime = Time.time + heartsDelay;
        }
    }
}
