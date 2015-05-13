using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Diploma : MonoBehaviour {

    public GameObject canvas;
    public Text title;
    public Text nameField;
    public Text emailField;
    public GameObject diplomaAsset;
    public Text username;
    public Image diploma1;
    public Image diploma2;
    private int diplomaID;

    private float lastVol;

	void Start () {
        canvas.SetActive(false);
	}
    public void Init(int id)
    {
        if (diplomaAsset)
        {
            diploma1.enabled = false;
            diploma2.enabled = false; 
            diplomaAsset.SetActive(false);
        }
        
        canvas.SetActive(true);
        this.diplomaID  = id;
        switch (id)
        {
            case 1: title.text = "YOU EARNED THE 'VOLCANO' DIPLOMA"; if (diplomaAsset) diploma1.enabled = true; break;
            case 2: title.text = "YOU EARNED THE 'FOREST' DIPLOMA"; if (diplomaAsset) diploma2.enabled = true; break;
        }
        lastVol = Data.Instance.musicVolume;
        Events.OnSoundFX("19_Ask Your Mom");
        Events.OnMusicVolumeChanged(0.3f);
    }
    public void OpenDiploma()
    {
        Close(nameField.text);
    }
    public void CloseCanvas()
    {
        canvas.SetActive(false);
        Events.OnSoundFX("backPress");
        Events.OnMusicVolumeChanged(lastVol);
    }
    public void Close(string _username)
    {
        if (diplomaAsset)
        {
            diplomaAsset.SetActive(true);
            if( GetComponent<Summary>() )
                Events.OnSoundFX("18_Congratulations");
        }
        username.text = _username;
        Events.OnMusicVolumeChanged(lastVol);
    }
    public void CloseDiplomaAsset()
    {
        print("CloseDiplomaAsset");
        Events.OnSoundFX("backPress");
        GetComponent<Summary>().diplomaClose();
        canvas.SetActive(false);
    }
    public void CloseDiplomaAssetInGallery()
    {
        canvas.SetActive(false);
    }
    public void Send()
    {
        Events.OnSoundFX("buttonPress");
        print("nameField: " + nameField.text + " email: " + emailField.text);
        StartCoroutine(SendMail());
        if (diplomaAsset)
            Close(nameField.text);
        else
            CloseCanvas();
    }
    public void SendLater()
    {
        Events.OnSoundFX("buttonPress");
        print("nameField: " + nameField.text + " email: " + emailField.text);
        Close(nameField.text);
    }

     IEnumerator SendMail()
     {

         string post_url = Data.Instance.URL_php_email + "email.php"
             + "?image=" + diplomaID.ToString()
             + "&to=" + WWW.EscapeURL(emailField.text)
             + "&from=" + WWW.EscapeURL(nameField.text);

         WWW receivedData = new WWW(post_url);
         yield return receivedData;

         if (receivedData.error != null)
             print("There was an error: " + receivedData.error);
         else
         {
             try
             {
                 print("SENDED" + receivedData.text);

             }
             catch
             {
                 Debug.Log("error sending");
             }
         }
     }


}
