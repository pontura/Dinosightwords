using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : MonoBehaviour {

	public GameObject panel;
	static IAPManager mInstance = null;
	public bool isUnlocked;

	public static IAPManager Instance
	{
		get
		{
			return mInstance;
		}
	}
	void Awake()
	{
		if (!mInstance)
			mInstance = this;
	}
	void Start () {
		
		if (PlayerPrefs.GetInt ("unlock")==1)
			isUnlocked = true;
		
		Close ();
		Events.SetPurchasesPanel += SetPurchasesPanel;
	}
	public void Close()
	{
		panel.SetActive (false);
	}
	void SetPurchasesPanel(bool isOn)
	{
		panel.SetActive (isOn);
	}
	public void OnIAPDone()
	{
		PlayerPrefs.SetInt ("unlock", 1);
		isUnlocked = true;
	}
	public void OnIAPError()
	{
		print ("Error");
	}
	public void ButtonClicked()
	{
		Close ();
	}
}
