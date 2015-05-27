using UnityEngine;
using System.Collections;

public class PrivacyPolicy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void Close () {
        Data.Instance.LoadLevel("02_MainMenu", 1, 1, Color.black);
        Events.OnSoundFX("backPress");
	}
}
