using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public GameObject canvas;
    public GameObject confirmCanvas;

	void Start () {
        canvas.SetActive(false);
        confirmCanvas.SetActive(false);
	}
    
    public void Init()
    {
        canvas.SetActive(true);
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
        Data.Instance.GetComponent<UserData>().Reset();
        confirmCanvas.SetActive(false);
        Close();
    }
}
