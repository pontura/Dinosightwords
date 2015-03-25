using UnityEngine;
using System.Collections;

public class VoicesManager : MonoBehaviour {

	void Start () {
        Events.OnVoice += OnVoice;
        print("SATATATS");
	}
	
	void OnDestroy () {
        Events.OnVoice -= OnVoice;
	}
    void OnVoice(string sightWord)
    {
        print(sightWord);
        audio.clip = Resources.Load("sightwords/" + sightWord) as AudioClip;
        audio.Play();

    }
}
