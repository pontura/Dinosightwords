using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public Text labelErrors;

    void Start()
    {
        int stars;
        int errors = Data.Instance.errors ;

        if (errors < 2)
            stars = 3;
        else if (errors < 4)
            stars = 2;
        else
            stars = 1;
        
        labelErrors.text = "STARS: " + stars + "(ERRORS " + errors  + ")";
    }
    public void ResetLevel()
    {
        Data.Instance.GetComponent<WordsData>().WordID = 1;
        Data.Instance.errors = 0;
    }
    public void Next()
    {
        ResetLevel();
        Data.Instance.GetComponent<WordsData>().LevelID++;        
        Application.LoadLevel("04_Game");
    }
    public void RePlay()
    {
        ResetLevel();
        Application.LoadLevel("04_Game");
    }
    public void MainMenu()
    {
        ResetLevel();
        Application.LoadLevel("02_MainMEnu");
    }
}
