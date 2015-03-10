using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    public int totalLanes;
    public int speed;
    public int laneYPosition;
    public int laneSeparation;
    public float timeToCrossLane;

    [Tooltip("X Position Of Hero. 0 is de Center")]
    public int CharacterXPosition;
    public float distanceBetweenWords;

    [Tooltip("Obstacles on or off")]
    public bool Obstacles;
    public float distanceBetweenObstacles;

    [Tooltip("Offset to separate Obstacles from Words")]
    public float offsetForObstacles;
}
