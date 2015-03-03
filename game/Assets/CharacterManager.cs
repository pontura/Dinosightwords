using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public Character character;
    public Lane[] lanes;
    public int laneID;

    public void Init()
    {
        Events.OnSwipe += OnSwipe;
        Events.OnChangeingLane += OnChangeingLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
    }
    public void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
        Events.OnChangeingLane -= OnChangeingLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
    }
    void OnSwipe(SwipeDetector.directions direction)
    {
        if (character.state == Character.states.CHANGE) return;
        if (laneID == 1 && direction == SwipeDetector.directions.UP) return;
        if (laneID == 4 && direction == SwipeDetector.directions.DOWN) return;

        switch (direction)
        {
            case SwipeDetector.directions.UP:
                character.MoveUP(); laneID--; break;
            case SwipeDetector.directions.DOWN:
                character.MoveDown(); laneID++; break;
        }
    }
    void OnChangeingLane()
    {
        character.transform.parent = GetActivetLane().gameObject.transform;
        character.GotoCenterOfLane();
    }
    void OnChangeLaneComplete()
    {

    }
    private Lane GetActivetLane()
    {
        return lanes[laneID-1];
    }
}
