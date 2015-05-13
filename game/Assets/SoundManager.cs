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
        print("OnSoundFX + " + soundName + " vol: " + Data.Instance.soundsVolume );
        if (soundName == "")
        {
            audio.Stop();
            return;
        }

        if (Data.Instance.soundsVolume == 0) return;

        audio.PlayOneShot(Resources.Load("sound/" + soundName) as AudioClip);

    }
}
