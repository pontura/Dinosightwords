using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryButton : MonoBehaviour {

    [SerializeField] Text SightWord;
    [SerializeField] Text Label;
    public Image diplomaImage;

    public int id;
    public string sightWord;
    public bool isActive;

    public void Init(Gallery gallery, string sightWord, bool _isActive, int levelToReachWord)
    {
        this.sightWord = sightWord;

        this.isActive = true;
        SightWord.text = sightWord;

        GetComponent<Button>().onClick.AddListener(() => OnClick());

        if (_isActive)
        {
           
        }
        else
        {
            this.isActive = false;
            SightWord.color = Color.gray;
           // Label.text = "Play level " + (levelToReachWord+1).ToString() + " to learn it!";
        }
        
    }
    void OnClick()
    {
        print("OnClick" + isActive);
        if (!isActive)
        {
            Events.OnSoundFX("denied");
        }
    }
    public void setDiplomaStatus(bool isActive)
    {
        this.isActive = isActive;
        if (!isActive)
            GetComponentInChildren<Image>().color = Color.grey;
    }
}
