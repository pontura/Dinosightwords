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
        INACTIVE
    }

    private WordsData wordsData;
    private float speed;
    private float realSpeed = 0;
    private LanesManager lanesManager;
    private WordsManager wordsManager;
    private float distanceBetweenWords;
    private float distanceBetweenObstacles;
    private float offsetForObstacles;

    public void Init()
    {
        Data.Instance.errors = 0;

        Events.OnPlayerHitWord += OnPlayerHitWord;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.OnLevelComplete += OnLevelComplete;
        Events.StartGame += StartGame;

        wordsData = Data.Instance.GetComponent<WordsData>();
        wordsData.Restart();

        lanesManager = GetComponent<LanesManager>();
        wordsManager = GetComponent<WordsManager>();
        lanesManager.AddLanes(Data.Instance.GetComponent<GameData>().totalLanes);
        GetComponent<CharacterManager>().Init();

        if (wordsData.GetZone() == 2)
        {
            foreach (GameObject go in Zone1Objects)
                go.SetActive(false);
        }
        else
        {
            foreach (GameObject go in Zone2Objects)
                go.SetActive(false);
        }
    }
    void OnDestroy()
    {
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.OnPlayerHitWord -= OnPlayerHitWord;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.StartGame -= StartGame;
    }
    void StartGame()
    {
        showObstacles = Data.Instance.gameData.Obstacles;
        distanceBetweenWords = Data.Instance.gameData.distanceBetweenWords;
        distanceBetweenObstacles = Data.Instance.gameData.distanceBetweenObstacles;
        offsetForObstacles = Data.Instance.gameData.offsetForObstacles;
        speed = Data.Instance.gameData.speed;

        state = states.ACTIVE;

        LoopWords();
        if (showObstacles)
            Invoke("LoopObstacles", offsetForObstacles + distanceBetweenObstacles);

        Events.OnNewWord(wordsData.GetWordData());

    }
   
    void OnLevelComplete()
    {
        state = states.INACTIVE;
        Events.OnMusicChange("victoryMusic");
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
        int num = Random.Range(0, 100);
        lanesManager.AddObject( PutWordObject() );
        LoopWords();
    }
    public void AddObstacle()
    {
        if (speed - realSpeed > 0.5f) { LoopObstacles(); return; }
        int num = Random.Range(0, 100);
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
    }
    void OnHeroSlide()
    {
        realSpeed*=2;
        Events.OnSoundFX("stepPond");
    }
    void Update()
    {
        if (state == states.INACTIVE)
        {
            return;
        }
        if (realSpeed < speed)
            realSpeed += 0.05f;
        else if (realSpeed > speed)
            realSpeed -= 0.05f;

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
