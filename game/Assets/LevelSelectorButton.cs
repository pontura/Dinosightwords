using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectorButton : MonoBehaviour {

    public bool isActive;
    public Stars stars;
    public Text label;
    public int id;
    public int zoneID;
    private Button button;

	public void Init(int zoneID, int id, int starsQTY)
    {
        this.zoneID = zoneID;
        this.id = id;

        label.text = id.ToString();
        stars.Init(starsQTY);
        button = GetComponent<Button>();
        isActive = true;

        if (starsQTY == 0)
        {
            isActive = false;
            button.targetGraphic.color = button.colors.disabledColor;
        }
	}
    public void NextButton()
    {
         isActive = true;
         button.targetGraphic.color = button.colors.normalColor;        
    }
}
