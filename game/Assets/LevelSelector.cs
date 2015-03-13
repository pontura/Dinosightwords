using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    public GameObject buttonsContainer;
    private UserData userData;
    public LevelLockedPopup levelLockedPopup;

	void Start () {
        levelLockedPopup.gameObject.SetActive(false);
        userData = Data.Instance.GetComponent<UserData>();
        int id = 1;
        LevelSelectorButton[] buttons = buttonsContainer.GetComponentsInChildren<LevelSelectorButton>();
        foreach (LevelSelectorButton button in buttons)
        {
            int starsQty = userData.GetStarsIn(1, id);
            button.Init(id, starsQty);
            id++;
        }
        buttons[0].GetComponent<LevelSelectorButton>().isActive = true;
	}
    public void Clicked(LevelSelectorButton button)
    {
        if (!button.isActive)
        {
            levelLockedPopup.gameObject.SetActive(true);
            return;
        }
        Data.Instance.GetComponent<WordsData>().LevelID = button.id;
        Application.LoadLevel("04_Game");
    }
    public void MainMenu()
    {
        Application.LoadLevel("02_MainMenu");
    }
}
