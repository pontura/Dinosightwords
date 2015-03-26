using UnityEngine;
using System.Collections;

public class VoicesManager : MonoBehaviour {

	void Start () {
        Events.OnVoice += OnVoice;
	}
	
	void OnDestroy () {
        Events.OnVoice -= OnVoice;
	}
    void OnVoice(string sightWord)
    {
        if (Data.Instance.soundsVolume == 0) return;

        print(sightWord);
        audio.clip = Resources.Load("sightwords/" + sightWord) as AudioClip;
        audio.Play();

    }
}
