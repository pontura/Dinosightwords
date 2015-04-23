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
            case 1: title.text = "YOU EARNED THE 'VULCANO' DIPLOMA"; if (diplomaAsset) diploma1.enabled = true; break;
            case 2: title.text = "YOU EARNED THE 'FOREST' DIPLOMA"; if (diplomaAsset) diploma2.enabled = true; break;
        }
    }
    public void OpenDiploma()
    {
        Close(nameField.text);
    }
    public void CloseCanvas()
    {
        canvas.SetActive(false);
    }
    public void Close(string _username)
    {
        if (diplomaAsset)
            diplomaAsset.SetActive(true);
        username.text = _username;
    }
    public void CloseDiplomaAsset()
    {
        GetComponent<Summary>().diplomaClose();
        canvas.SetActive(false);
    }
    public void Send()
    {
        print("nameField: " + nameField.text + " email: " + emailField.text);
        StartCoroutine(SendMail());
        if (diplomaAsset)
            Close(nameField.text);
        else
            CloseCanvas();
    }
    public void SendLater()
    {
        print("nameField: " + nameField.text + " email: " + emailField.text);
        Close(nameField.text);
    }

     IEnumerator SendMail()
     {
         string message = nameField.text + " won a diploma in DinoSightWords!";

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
