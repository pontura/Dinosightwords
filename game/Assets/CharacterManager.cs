using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public Character character;
    private LanesManager lanesManager;
    

    public void Init()
    {
        Events.OnSwipe += OnSwipe;
        Events.OnChangeingLane += OnChangeingLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
        lanesManager = Game.Instance.GetComponent<LanesManager>();
        OnChangeingLane();
        character.Init();
    }
    public void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
        Events.OnChangeingLane -= OnChangeingLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
    }
    void OnSwipe(SwipeDetector.directions direction)
    {
        if ( Game.Instance.state != Game.states.PLAYING ) return;
        if (character.state == Character.states.CHANGE) return;
        if (lanesManager.laneActiveID == 1 && direction == SwipeDetector.directions.UP) return;
        if (lanesManager.laneActiveID == Data.Instance.GetComponent<GameData>().totalLanes && direction == SwipeDetector.directions.DOWN) return;

        switch (direction)
        {
            case SwipeDetector.directions.UP:
                character.MoveUP(); lanesManager.laneActiveID--; break;
            case SwipeDetector.directions.DOWN:
                character.MoveDown(); lanesManager.laneActiveID++; break;
        }
    }
    void OnChangeingLane()
    {
        if (lanesManager.GetActivetLane())
        {
            character.transform.parent = lanesManager.GetActivetLane().gameObject.transform;
            character.GotoCenterOfLane();
        }
    }
    void OnChangeLaneComplete()
    {

    }
    
}
