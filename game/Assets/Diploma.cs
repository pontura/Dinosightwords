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

	void Start () {
        canvas.SetActive(false);
	}
    public void Init(int id)
    {
        diplomaAsset.SetActive(false);
        diploma1.enabled = false;
        diploma2.enabled = false; 
        canvas.SetActive(true);
        switch (id)
        {
            case 1: title.text = "YOU EARNED THE 'FOREST' DIPLOMA"; diploma1.enabled = true;  break;
            case 2: title.text = "YOU EARNED THE 'VULCANO' DIPLOMA"; diploma2.enabled = true; break;
        }
    }
    public void Close(string _username)
    {
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
        Close(nameField.text);
    }
    public void SendLater()
    {
        print("nameField: " + nameField.text + " email: " + emailField.text);
        Close(nameField.text);
    }
}
