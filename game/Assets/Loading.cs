using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	public GameObject toDestroy;

	void Start () {
		GameObject.DontDestroyOnLoad (this);
		Invoke ("LoadNext", 0.5f);
	}
	void LoadNext()
	{
		SceneManager.LoadScene ("01_Splash");
		Invoke ("Clear", 1);
	}
	void Clear()
	{
		toDestroy.SetActive( false );
	}
	public void Close()
	{
		Application.Quit ();
	}
}
