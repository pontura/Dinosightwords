using UnityEngine;
using System.Collections;

public class HeroClothes : MonoBehaviour {

    public GameObject[] hats;
    public GameObject[] chairs;
    public GameObject[] legs;
    public GameObject[] legs2;

	void Start () {
        if (PlayerPrefs.GetInt("hats") > 0)
            OnHeroWinClothes("hats", PlayerPrefs.GetInt("hats"));
        if (PlayerPrefs.GetInt("chairs") > 0)
            OnHeroWinClothes("chairs", PlayerPrefs.GetInt("chairs"));
        if (PlayerPrefs.GetInt("legs") > 0)
            OnHeroWinClothes("legs", PlayerPrefs.GetInt("legs"));
    }

    void OnHeroWinClothes(string type, int num)
    {
        GameObject[] clothes;
        switch (type)
        {
            case "hats": clothes = hats; break;
            case "chairs": clothes = chairs; break;
            default: clothes = legs; break;
        }
        
        foreach (GameObject cloth in clothes)
            cloth.SetActive(false);

        clothes[num - 1].SetActive(true);

        if (type == "legs")
        {
            foreach (GameObject cloth in legs2)
                cloth.SetActive(false);
            legs2[num - 1].SetActive(true);
        }

    }
}
