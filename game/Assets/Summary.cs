using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public Text labelErrors;
    [SerializeField] Canvas canvas;
    [SerializeField] Stars stars;

    void Start()
    {
        Events.OnLevelComplete += OnLevelComplete;
        canvas.enabled = false;
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void OnLevelComplete()
    {
        canvas.enabled = true;
        int _stars;
        int errors = Data.Instance.errors;

        if (errors < 2)
            _stars = 3;
        else if (errors < 4)
            _stars = 2;
        else
            _stars = 1;
        
        labelErrors.text = "(" + errors  + " errors)";
        stars.Init(_stars);
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
