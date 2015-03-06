using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Lane : MonoBehaviour {

    public float distance;
    public int id;
    public GameObject objectsTarget;
    public List<LaneObject> laneObjects;

    public void AddObject(LaneObject laneObject, LaneObjectData data)
    {
        LaneObject newLaneObject = Instantiate(laneObject, Vector3.zero, Quaternion.identity) as LaneObject;
        
        laneObjects.Add(newLaneObject);
        newLaneObject.transform.parent = objectsTarget.transform;
        newLaneObject.transform.localScale = Vector3.one;
        newLaneObject.transform.localPosition = new Vector3( 1200, 0, 0);
        newLaneObject.SetData(data);
        
    }
    void DeleteObject(LaneObject laneObject)
    {
        laneObjects.Remove(laneObject);
        laneObject.Destroy();
    }
    public void Move( float _x )
    {
        distance += _x;
        for (int i = 0; i < laneObjects.Count; i++)
        {
            LaneObject laneObject = laneObjects[i];
            Vector3 pos = laneObject.transform.localPosition;
            pos.x -= _x;
            laneObject.transform.localPosition = pos;
            if (pos.x < -1200) DeleteObject(laneObject);
        }
    }
}
