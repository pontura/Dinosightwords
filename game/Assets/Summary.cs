using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public GameObject canvas;
    [SerializeField] Stars stars;
    private string NextAction;

    public GameObject RewardsCanvas;

    public GameObject[] rewardHats;
    public GameObject[] rewardChairs;
    public GameObject[] rewardHShoes;

    public AudioClip[] clipHats;
    public AudioClip[] clipChairs;
    public AudioClip[] clipShoes;

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

        if (errors==0)
            _stars = 3;
        else if (errors==1)
            _stars = 2;
        else
            _stars = 1;
        
        stars.Init(_stars);

        Invoke("sayCongrats", 1);
    }
    void sayCongrats()
    {
        int rand = Random.Range(0, 100);

        if (rand < 33)
            Events.OnSoundFX("2_Well Done");
        else if (rand < 66)
            Events.OnSoundFX("3_Great Job");
        else
            Events.OnSoundFX("4_Awesome Job");
    }
    private WordsData.Reward reward;
    private AudioClip clip;
    void OnReward(WordsData.Reward reward)
    {
        this.reward = reward;
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
            case "hats": _item = rewardHats[reward.num - 1]; clip = clipHats[reward.num - 1]; break;
            case "chairs": _item = rewardChairs[reward.num - 1]; clip = clipChairs[reward.num - 1]; break;
            default: _item = rewardHShoes[reward.num - 1]; clip = clipShoes[reward.num - 1];  break;
        }
        
        _item.SetActive(true);
        Invoke("particlesOn", 2f);
    }
    void particlesOn()
    {
        Events.OnHeroWinClothes(reward.rewardType, reward.num);
        Invoke("sayIt", 1);
    }
    void sayIt()
    {
        Events.OnSoundFX(clip.name);
    }
    public void ResetLevel()
    {
        Data.Instance.GetComponent<WordsData>().WordID = 1;
        Data.Instance.errors = 0;
    }
    public void Next()
    {
        Events.OnSoundFX("buttonPress");
		print ("ASDASSAD");
        if ( !OpenDiploma("Next"))
        {
            if (Data.Instance.GetComponent<WordsData>().LevelID < 30)
            {
                Data.Instance.GetComponent<WordsData>().LevelID++;
                Data.Instance.LoadLevel("04_Game", 1, 1, Color.black);
            }
            else
            {
                Data.Instance.LoadLevel("02_MainMenu", 1, 1, Color.black);
            }
        }
        ResetLevel();
    }
    public void RePlay()
    {
        Events.OnSoundFX("buttonPress");
        if (!OpenDiploma("RePlay"))
        {
            Data.Instance.LoadLevel("04_Game", 1, 1, Color.black);
        }
        ResetLevel();
    }
    public void MainMenu()
    {
        Events.OnSoundFX("buttonPress");
        if (!OpenDiploma("MainMenu"))
        {
            
            Data.Instance.LoadLevel("03_LevelSelector", 1, 1, Color.black);
        }
        ResetLevel();
    }
    public bool OpenDiploma(string NextAction)
    {
        Events.OnSoundFX("buttonPress");
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
        Events.OnSoundFX("backPress");
        switch(NextAction)
        {
            case "Next": Next(); break;
            case "MainMenu": MainMenu(); break;
            case "RePlay": RePlay(); break;
        }
    }
}
