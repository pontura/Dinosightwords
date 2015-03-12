using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectorButton : MonoBehaviour {

    public Stars stars;
    public Text label;
    public int id;

	public void Init(int id, int starsQTY)
    {
        this.id = id;
        label.text = id.ToString();
        stars.Init(starsQTY);
	}
}
