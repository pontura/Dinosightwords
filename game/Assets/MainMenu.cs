using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void Play () {
        Application.LoadLevel("03_LevelSelector");
	}
    public void Gallery()
    {
        Application.LoadLevel("06_Gallery");
    }
}
