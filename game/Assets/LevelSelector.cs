using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    public GameObject buttonsContainer;
    private UserData userData;

	void Start () {
        userData = Data.Instance.GetComponent<UserData>();
        int id = 1;
        LevelSelectorButton[] buttons = buttonsContainer.GetComponentsInChildren<LevelSelectorButton>();
        foreach (LevelSelectorButton button in buttons)
        {
            int starsQty = userData.GetStarsIn(1, id);
            button.Init(id, starsQty);
            id++;
        }
	}
    public void Clicked(LevelSelectorButton button)
    {
        Data.Instance.GetComponent<WordsData>().LevelID = button.id;
        Application.LoadLevel("04_Game");
    }
}
