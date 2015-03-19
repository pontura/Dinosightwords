using UnityEngine;
using System.Collections;

public class VoicesManager : MonoBehaviour {

	void Start () {
        Events.OnNewWord += OnNewWord;
	}
	
	void OnDestroy () {
        Events.OnNewWord -= OnNewWord;
	}

    void OnNewWord(WordsData.Word word)
    {
        string sightWord = word.sightWord;
        print(sightWord);
        audio.clip = Resources.Load("sightwords/" + sightWord) as AudioClip;
        audio.Play();
    }
}
