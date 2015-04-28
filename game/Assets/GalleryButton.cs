using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryButton : MonoBehaviour {

    [SerializeField] Text SightWord;
    [SerializeField] Text Label;
    [SerializeField] GameObject isOn;
    [SerializeField] GameObject isOff;
    public Image diplomaImage;

    public int id;
    public string sightWord;
    public bool isActive;

    public void Init(Gallery gallery, string sightWord, bool _isActive, int levelToReachWord)
    {
        this.sightWord = sightWord;

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
            Label.text = "Play level " + (levelToReachWord+1).ToString() + " to learn it!";
        }
    }
    public void setDiplomaStatus(bool isActive)
    {
        this.isActive = isActive;
        if (!isActive)
            GetComponentInChildren<Image>().color = Color.grey;
    }
}
