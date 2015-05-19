using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float distance;
    public int score;
    public Scrolleable[] Scrolleables;

    public GameObject[] Zone1Objects;
    public GameObject[] Zone2Objects;

    private bool showObstacles;

    public states state;
    public enum states
    {
        IDLE,
        ACTIVE,
        INACTIVE,
        GAMEOVER
    }

    private WordsData wordsData;
    private float speed;
    public float realSpeed = 0;
    public int percentProbabilityObstacle;
    private LanesManager lanesManager;
    private float distanceBetweenWords;
    private float distanceBetweenObstacles;
    private float offsetForObstacles;
    private float lastVolume;

    public void Init()
    {
        lastVolume = Data.Instance.musicVolume;
        Data.Instance.errors = 0;

        Events.OnPlayerHitWord += OnPlayerHitWord;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.OnLevelComplete += OnLevelComplete;
        Events.StartGame += StartGame;
        Events.OnGameOver += OnGameOver;

        wordsData = Data.Instance.GetComponent<WordsData>();
        wordsData.Restart();

        lanesManager = GetComponent<LanesManager>();
        lanesManager.AddLanes(Data.Instance.GetComponent<GameData>().totalLanes);
        GetComponent<CharacterManager>().Init();

        if (wordsData.GetZone() == 2)
        {
            foreach (GameObject go in Zone1Objects)
                go.SetActive(true);
            foreach (GameObject go in Zone1Objects)
                go.SetActive(false);
        }
        else
        {
            foreach (GameObject go in Zone2Objects)
                go.SetActive(true);
            foreach (GameObject go in Zone2Objects)
                go.SetActive(false);
        }

        if (Data.Instance.GetComponent<WordsData>().LevelID > 1) Data.Instance.TutorialReady = true;
        if (Data.Instance.TutorialReady)
            TutorialReady();
        else
            Invoke("SayTutorial", 0.1f);

    }
    void SayTutorial()
    {
        if (Random.Range(0, 100) < 50)
            Events.OnSoundFX("26_SwipeUpAndDown");
        else
            Events.OnSoundFX("27_YouLoseALife");

        Data.Instance.TutorialReady = true;
        Invoke("TutorialReady", 6);
    }
    void TutorialReady()
    {
        WordsData.Reward reward = wordsData.GetReward();
        if (reward.num > 0)
            Events.CheckItemsToReward(reward);
        else
            Events.OnStartCountDown();
    }
    void OnDestroy()
    {
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.OnPlayerHitWord -= OnPlayerHitWord;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.StartGame -= StartGame;
        Events.OnGameOver -= OnGameOver;

        Events.OnMusicVolumeChanged(lastVolume);
    }
    void StartGame()
    {
        showObstacles = Data.Instance.gameData.Obstacles;
        distanceBetweenWords = Data.Instance.gameData.distanceBetweenWords;
        distanceBetweenObstacles = Data.Instance.gameData.distanceBetweenObstacles;
        offsetForObstacles = Data.Instance.gameData.offsetForObstacles;

        float speedFrom = Data.Instance.gameData.speedFrom;
        float speedTo = Data.Instance.gameData.speedTo;
        float Diff = speedTo - speedFrom;
        int total = wordsData.Zone1.Length;
        int actual = wordsData.LevelID;
        speed = speedFrom + (actual * Diff / total);

        int percentProbabilityFrom = Data.Instance.gameData.percentProbabilityObstacleFrom;
        int percentProbabilityTo = Data.Instance.gameData.percentProbabilityObstacleTo;
        int Diff2 = percentProbabilityTo - percentProbabilityFrom;
        percentProbabilityObstacle = percentProbabilityFrom + (actual * Diff2 / total);

        state = states.ACTIVE;

        LoopWords();
        if (showObstacles)
            Invoke("LoopObstacles", offsetForObstacles + distanceBetweenObstacles);

        Events.OnNewWord(wordsData.GetWordData());
        
    }
    void OnGameOver()
    {
        state = states.GAMEOVER;
        Events.OnSoundFX("warningPopUp");
        Events.OnMusicChange("gameOverTemp");
        Invoke("voice", 2);
    }
    void voice()
    {
        Events.OnSoundFX("23_Try Again");
    }
    void OnLevelComplete()
    {
        state = states.INACTIVE;
        lastVolume = Data.Instance.musicVolume;
        Events.OnMusicVolumeChanged(0.2f);
        Events.OnSoundFX("victoryMusic");
    }
    void OnPlayerHitWord(LaneObjectData data)
    {
        if (data.score < 0)
        {
            Data.Instance.errors++;
            Events.OnHeroUnhappy();
            Events.OnSoundFX("mistakeWord");
        }
        else
        {
            Events.OnHeroCelebrate();
            Events.OnSoundFX("correctWord");
            Events.OnVoice(wordsData.GetWordData().sightWord);
        }

        score += data.score;
        if (score < 0) score = 0;
        Events.OnScoreRefresh(score);

        if (score == wordsData.GetScoreCurrentLevel())
            Events.OnLevelComplete();
        else if (score >= wordsData.nextScore)
            Events.SetNextWord();
    }
    public void LoopWords()
    {
        Invoke("AddWord", distanceBetweenWords);
    }
    public void LoopObstacles()
    {
        Invoke("AddObstacle", distanceBetweenObstacles);
    }
    public void AddWord()
    {
        if (speed - realSpeed > 0.5f) { LoopWords(); return; }
        lanesManager.AddObject( PutWordObject() );
        LoopWords();
    }
    public void AddObstacle()
    {
        if (Random.Range(0, 100) > percentProbabilityObstacle) { LoopObstacles(); return; }
        if (speed - realSpeed > 0.5f) { LoopObstacles(); return; }
        lanesManager.AddObject(PutObstacleObject());
        LoopObstacles();
    }
    private LaneObject PutObstacleObject()
    {
        try
        {
            return Game.Instance.GetComponent<ObstaclesManager>().GetNewObject();
        } catch
        {
            Debug.LogError("No Word");
        }
        return null;
    }
    private LaneObject PutWordObject()
    {
          try
        {
            return Game.Instance.GetComponent<WordsManager>().GetNewObject();
        }
          catch
        {
            Debug.LogError("No Object");
        }
          return null;
    }
    void OnHeroCrash()
    {
        Data.Instance.errors++;
        realSpeed = 0;
        Events.OnSoundFX("trip");
        if (state != states.GAMEOVER)
        {
            state = states.INACTIVE;
            Invoke("goOn", 1.7f);
        }
    }
    void goOn()
    {
        if (state == states.GAMEOVER) return;
        state = states.ACTIVE;
    }
    void OnHeroSlide(int id)
    {
        realSpeed = speed*2;
        Events.OnSoundFX("stepPond");
    }
    void Update()
    {
        if (state == states.INACTIVE || state == states.GAMEOVER)
        {
            return;
        }
        if (realSpeed < speed)
            realSpeed += 0.04f;
        else if (realSpeed > speed)
            realSpeed -= 0.04f;

        if (state == states.ACTIVE)
        {
            float _speed = (realSpeed * 100) * Time.deltaTime;
           // float _speed = speed*2;
            distance += _speed;
            lanesManager.MoveLanes(_speed);

           // characterHero.Move(_speed);

            foreach (Scrolleable scrolleable in Scrolleables)
            {
                scrolleable.Move(_speed);
            }
        }
    }
}
