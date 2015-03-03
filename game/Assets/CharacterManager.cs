using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public Character character;

    public void Init()
    {
        Events.OnSwipe += OnSwipe;
    }
    public void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
    }
    void OnSwipe(SwipeDetector.directions direction)
    {
        print(direction);
        switch (direction)
        {
            case SwipeDetector.directions.UP:
                character.MoveUP(); break;
            case SwipeDetector.directions.DOWN:
                character.MoveDown(); break;
        }
    }
}
