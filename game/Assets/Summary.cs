using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public Text labelErrors;
    public GameObject canvas;
    [SerializeField] Stars stars;
    private string NextAction;

    public GameObject RewardsCanvas;
    public GameObject[] rewardHats;
    public GameObject[] rewardChairs;
    public GameObject[] rewardHShoes;

    void Start()
    {
        Events.OnLevelComplete += OnLevelComplete;
        Events.OnReward += OnReward;
        canvas.SetActive(false);
        RewardsCanvas.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnReward -= OnReward;
    }
    void OnLevelComplete()
    {
        canvas.SetActive(true);
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
    void OnReward(WordsData.Reward reward)
    {
        canvas.SetActive(true);
        RewardsCanvas.SetActive(true);
        foreach (GameObject item in rewardHats)
            item.SetActive(false);
        foreach (GameObject item in rewardChairs)
            item.SetActive(false);
        foreach (GameObject item in rewardHShoes)
            item.SetActive(false);
        Vector3 pos = canvas.transform.localPosition;
        pos.y = 134;
        canvas.transform.localPosition = pos;

        GameObject _item;
        switch (reward.rewardType)
        {
            case "hats": _item = rewardHats[reward.num - 1]; break;
            case "chairs": _item = rewardChairs[reward.num - 1]; break;
            default: _item = rewardHShoes[reward.num - 1]; break;
        }
        _item.SetActive(true);
    }
    public void ResetLevel()
    {
        Data.Instance.GetComponent<WordsData>().WordID = 1;
        Data.Instance.errors = 0;
    }
    public void Next()
    {
        if ( !OpenDiploma("Next"))
        {
            if (Data.Instance.GetComponent<WordsData>().LevelID < 30)
            {
                Data.Instance.GetComponent<WordsData>().LevelID++;
                Application.LoadLevel("04_Game");
            }
            else
            {
                Application.LoadLevel("02_MainMenu");
            }
        }
        ResetLevel();
    }
    public void RePlay()
    {
        if (!OpenDiploma("RePlay"))
        {
            Application.LoadLevel("04_Game");
        }
        ResetLevel();
    }
    public void MainMenu()
    {
        if (!OpenDiploma("MainMenu"))
        {
            Application.LoadLevel("03_LevelSelector");
        }
        ResetLevel();
    }
    public bool OpenDiploma(string NextAction)
    {
        bool opened = false;
        int id = 0;
        if (Data.Instance.GetComponent<UserData>().diplomaId <1 && Data.Instance.GetComponent<WordsData>().LevelID == 15)
        {
            opened = true;
            id = 1;
        }
        else if (Data.Instance.GetComponent<UserData>().diplomaId <2 && Data.Instance.GetComponent<WordsData>().LevelID == 30)
        {
            opened = true;
            id = 2;
        }
        if(opened)
        {
            Events.WinDiploma(id);
            this.NextAction = NextAction;
            GetComponent<Diploma>().Init(id);
            canvas.SetActive(false);
        }
        return opened;
    }
    public void diplomaClose()
    {
        print("diplomaClose" + NextAction);
        switch(NextAction)
        {
            case "Next": Next(); break;
            case "MainMenu": MainMenu(); break;
            case "RePlay": RePlay(); break;
        }
    }
}
