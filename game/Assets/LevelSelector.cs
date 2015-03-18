using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    [SerializeField]
    Button back;
    [SerializeField]
    Button next;
    [SerializeField]
    Text title;

    public GameObject buttonsContainer;

    private UserData userData;
    public LevelLockedPopup levelLockedPopup;
    public int zoneID = 1;
    
    private int id = 1;
    private bool nextActiveButton = false;
    private int _xZone2 = -800;

	void Start () {
        activateZone(1);
        levelLockedPopup.gameObject.SetActive(false);
        userData = Data.Instance.GetComponent<UserData>();

        LevelSelectorButton[] buttons = buttonsContainer.GetComponentsInChildren<LevelSelectorButton>();
        
        foreach (LevelSelectorButton button in buttons)
        {
            
            int starsQty = userData.GetStarsIn( id);

            button.Init(zoneID, id, starsQty);

            //el proximo activo:
            if (starsQty == 0 && !nextActiveButton)
            {
                nextActiveButton = true;
                button.NextButton();
            }

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
    public void nextClicked()
    {
        activateZone(2);
    }
    public void prevClicked()
    {
        activateZone(1);
    }
    private void activateZone(int zoneID)
    {       

        this.zoneID = zoneID;
        Vector3 pos = buttonsContainer.transform.localPosition;
        string _title;
        if (zoneID == 2)
        {
            pos.x = _xZone2;
            _title = "Volcano";
        }
        else
        {
            pos.x = 0;
            _title = "Forest";
        }
        buttonsContainer.transform.localPosition = pos;

        title.text = "Zone " + zoneID + ":" + _title;
    }
    
}
