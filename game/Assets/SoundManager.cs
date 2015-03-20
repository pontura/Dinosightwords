using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    void Start()
    {
        Events.OnSoundFX += OnSoundFX;
    }

    void OnDestroy()
    {
        Events.OnSoundFX -= OnSoundFX;
    }

    void OnSoundFX(string soundName)
    {
        audio.clip = Resources.Load("sound/" + soundName) as AudioClip;
        audio.Play();
    }
}
