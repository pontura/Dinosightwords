using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryButton : MonoBehaviour {

    [SerializeField] Text SightWord;
    [SerializeField] Text Label;
    [SerializeField] GameObject isOn;
    [SerializeField] GameObject isOff;

    public int id;
    public string sightWord;
    public bool isActive;
    private Gallery gallery;    

    public void Init(Gallery gallery, string sightWord, bool _isActive, int levelToReachWord)
    {
        this.sightWord = sightWord;
        this.gallery = gallery;

        isOn.SetActive(false);
        isOff.SetActive(false);

        if (_isActive)
        {
            this.isActive = true;
            SightWord.text = sightWord;
            isOn.SetActive(true);
        }
        else
        {
            this.isActive = false;
            isOff.SetActive(true);
            SightWord.text = "???";
            Label.text = "play level " + levelToReachWord + " to learn it!";
        }
    }
}
