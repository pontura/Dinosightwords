using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LanesManager : MonoBehaviour {

    public Lane lane;
    public List<Lane> lanes;
    public Transform canvasContainer;
    public int laneActiveID = 0;

    public void AddLanes(int qty)
    {
        int laneSeparation = Data.Instance.gameData.laneSeparation;
        int laneYPosition = -Data.Instance.gameData.laneYPosition;
        for (int a = 0; a < qty+1; a++)
        {
            Lane newLane = Instantiate(lane, Vector3.zero, Quaternion.identity) as Lane;
            lanes.Add(newLane);
            newLane.transform.SetParent(canvasContainer.transform);
            newLane.GetComponent<RectTransform>().anchoredPosition = new Vector3(-800, laneYPosition - (laneSeparation * a), 0);
            newLane.GetComponent<RectTransform>().sizeDelta = new Vector3(100,100,100);
            newLane.GetComponent<RectTransform>().localScale = Vector3.one;
            newLane.Init(a,Data.Instance.GetComponent<WordsData>().GetZone());
        }
    }
    public void AddObject(LaneObject laneObject)
    {
        if (laneObject == null)
            Debug.LogError("No Object to add");
        else
            GetRandomLane().AddObject(laneObject);
    }
    public Lane GetActivetLane()
    {
        return lanes[laneActiveID - 1];
    }
    public Lane GetRandomLane()
    {
        return lanes[Random.Range(0,lanes.Count-1)];
    }
    public void MoveLanes(float _x)
    {
        foreach (Lane lane in lanes)
        {
            lane.Move(_x);
        }
    }
}
