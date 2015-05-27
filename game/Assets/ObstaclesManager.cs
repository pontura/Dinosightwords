using UnityEngine;
using System.Collections;

public class ObstaclesManager : MonoBehaviour {

    public LaneObject[] obstacles;

    public LaneObject GetNewObject()
    {
        LaneObject laneObject;  
        int random = Random.Range(0, obstacles.Length);
        laneObject = obstacles[random];

        LaneObjectData data = new LaneObjectData();
        data.score = -1;
        laneObject.data = data;

        return laneObject;
    }
}
