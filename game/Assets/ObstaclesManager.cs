using UnityEngine;
using System.Collections;

public class ObstaclesManager : MonoBehaviour {

    public LaneObject[] obstacles;

    public LaneObject GetNewObject()
    {
        LaneObject laneObject;        
        laneObject = obstacles[0];

        LaneObjectData data = new LaneObjectData();
        data.score = -1;
        laneObject.data = data;

        return laneObject;
    }
}
