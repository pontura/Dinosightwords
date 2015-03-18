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
        print("VoicesManager say: " + sightWord);
        audio.clip = Resources.Load("sightwords/" + sightWord) as AudioClip;
        audio.Play();
    }
}
