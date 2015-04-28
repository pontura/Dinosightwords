using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    public GameObject canvas;
    public GameObject confirmCanvas;
    public Text inputText;
    public GameObject WrongCanvas;

	void Start () {
        canvas.SetActive(false);
        confirmCanvas.SetActive(false);
        WrongCanvas.SetActive(false);
	}
    
    public void Init()
    {
        canvas.SetActive(true);
        GetComponent<PausedMenu>().Init();
    }

    public void Privacy()
    {
        Application.LoadLevel("07_PrivacyPolicy");
    }

    public void Reset()
    {
        confirmCanvas.SetActive(true);
        Close();
    }

    public void Close()
    {
        canvas.SetActive(false);
        
    }
    public void Confirm()
    {
        confirmCanvas.SetActive(true);
        Close();
    }
    public void CloseConfirm()
    {
        confirmCanvas.SetActive(false);
    }
    public void ConfirmReset()
    {
        if (inputText.text == "56")
        {
            Data.Instance.GetComponent<UserData>().Reset();
            confirmCanvas.SetActive(false);
            Close();
        }
        else
        {
            WrongCanvas.SetActive(true);
        }
    }
    public void CloseWrong()
    {
        WrongCanvas.SetActive(false);
    }
}
