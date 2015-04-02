using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    public int totalLanes;

    [Tooltip("From level 1")]
    public int speedFrom;
    [Tooltip("To level 30")]
    public int speedTo;

    public int laneYPosition;
    public int laneSeparation;
    public float timeToCrossLane;

    [Tooltip("X Position Of Hero. 0 is de Center")]
    public int CharacterXPosition;
    public float distanceBetweenWords;

    [Tooltip("Obstacles on or off")]
    public bool Obstacles;
    public float distanceBetweenObstacles;
    public int percentProbabilityObstacleFrom;
    public int percentProbabilityObstacleTo;

    [Tooltip("Offset to separate Obstacles from Words")]
    public float offsetForObstacles;
}
