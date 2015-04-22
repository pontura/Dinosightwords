using UnityEngine;
using System.Collections;

public class PrivacyPolicy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void Close () {
        Application.LoadLevel("02_MainMenu");
	}
}
