using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float distance;
    public states state;

    public Scrolleable[] Scrolleables;

    private bool showObstacles;

    public enum states
    {
        IDLE,
        ACTIVE,
        INACTIVE
    }

    private WordsData wordsData;
    private int speed;    
    private LanesManager lanesManager;
    private float distanceBetweenWords;
    private float distanceBetweenObstacles;
    private float offsetForObstacles;

    public void Init()
    {
        wordsData = Data.Instance.GetComponent<WordsData>();
        lanesManager = GetComponent<LanesManager>();
        lanesManager.AddLanes(Data.Instance.GetComponent<GameData>().totalLanes);
        GetComponent<CharacterManager>().Init();
        showObstacles = Data.Instance.gameData.Obstacles;
        distanceBetweenWords = Data.Instance.gameData.distanceBetweenWords;
        distanceBetweenObstacles = Data.Instance.gameData.distanceBetweenObstacles;
        offsetForObstacles = Data.Instance.gameData.offsetForObstacles;
        speed = Data.Instance.gameData.speed;

        state = states.ACTIVE;

        LoopWords();
        if (showObstacles)
            Invoke("LoopObstacles", offsetForObstacles + distanceBetweenObstacles);
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
        int num = Random.Range(0, 100);
        lanesManager.AddObject( PutWordObject() );
        LoopWords();
    }
    public void AddObstacle()
    {
        int num = Random.Range(0, 100);
        lanesManager.AddObject(PutObstacleObject());
        LoopObstacles();
    }
    private LaneObject PutObstacleObject()
    {
        return Game.Instance.GetComponent<ObstaclesManager>().GetNewObject();
    }
    private LaneObject PutWordObject()
    {
        return Game.Instance.GetComponent<WordsManager>().GetNewObject();
    }
    void Update()
    {
        if (state == states.ACTIVE)
        {
            float _speed = (speed * 100) * Time.deltaTime;
            distance += _speed;
            lanesManager.MoveLanes(_speed);

            foreach (Scrolleable scrolleable in Scrolleables)
            {
                scrolleable.Move(_speed);
            }
        }
    }
}
