using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	private AudioSource audioSource;
    void Start()
    {
		audioSource = GetComponent<AudioSource> ();
        Events.OnSoundFX += OnSoundFX;
    }

    void OnDestroy()
    {
        Events.OnSoundFX -= OnSoundFX;
    }

    void OnSoundFX(string soundName)
    {
		print ("OnSoundFX: " + soundName);

        if (soundName == "")
        {
            GetComponent<AudioSource>().Stop();
            return;
        }

        if (Data.Instance.soundsVolume == 0) return;
		AudioClip ac = Resources.Load("sound/" + soundName) as AudioClip;
		audioSource.PlayOneShot(ac,  1f);

		print ("AudioClip: " + ac);

    }
}
