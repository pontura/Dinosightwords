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
		print ("voice onVoice: " + sightWord);
        if (Data.Instance.soundsVolume == 0) return;

        GetComponent<AudioSource>().clip = Resources.Load("sightwords/" + sightWord) as AudioClip;
        GetComponent<AudioSource>().Play();

    }
}
