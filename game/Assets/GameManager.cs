using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float distance;
    public states state;

    public LaneObject LaneObject_Word;

    public enum states
    {
        IDLE,
        ACTIVE,
        INACTIVE
    }

    private int speed;    
    private LanesManager lanesManager;
   

    public void Init()
    {
        lanesManager = GetComponent<LanesManager>();
        lanesManager.AddLanes(Data.Instance.GetComponent<GameData>().totalLanes);
        GetComponent<CharacterManager>().Init();
        speed = Data.Instance.gameData.speed;
        Loop();
        state = states.ACTIVE;
    }
    public void Loop()
    {
        Invoke("AddObject", 2);
    }
    public void AddObject()
    {
        lanesManager.AddObject(LaneObject_Word);
        Loop();
    }
    void Update()
    {
        if (state == states.ACTIVE)
        {
            float _speed = (speed * 100) * Time.deltaTime;
            distance += _speed;
            lanesManager.MoveLanes(_speed);
        }
    }
}
