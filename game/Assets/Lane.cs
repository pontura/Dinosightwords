using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Lane : MonoBehaviour {

    public GameObject[] Floor;
    public LaneObject border1;
    public LaneObject border2;
    public float distance;
    public int id;
    public GameObject objectsTarget;
    public List<LaneObject> laneObjects;

    public void Init(int id, int ZoneID)
    {
        this.id = id;
        LaneObject newLaneObject;
        if (ZoneID == 1)
            newLaneObject = Instantiate(border1, Vector3.zero, Quaternion.identity) as LaneObject;
        else
            newLaneObject = Instantiate(border2, Vector3.zero, Quaternion.identity) as LaneObject;

        laneObjects.Add(newLaneObject);
        newLaneObject.transform.SetParent(objectsTarget.transform);
        newLaneObject.transform.localScale = Vector3.one;
        newLaneObject.transform.localPosition = new Vector3((id+1) * 60, 0, 0);
        newLaneObject.repeatIn = (int)newLaneObject.transform.localPosition.x;


        if (id % 2 != 0)
        {
           // Floor[0].gameObject.SetActive(true);
           // Floor[1].gameObject.SetActive(false);
            newLaneObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
           // Floor[1].gameObject.SetActive(true);
          //  Floor[0].gameObject.SetActive(false);
        }
    }
    public void AddObject(LaneObject laneObject)
    {
        LaneObject newLaneObject = Instantiate(laneObject, Vector3.zero, Quaternion.identity) as LaneObject;
        
        laneObjects.Add(newLaneObject);
        newLaneObject.transform.SetParent( objectsTarget.transform );
        newLaneObject.transform.localScale = Vector3.one;
        newLaneObject.transform.localPosition = new Vector3( 1200, 0, 0);
        newLaneObject.SetData(laneObject.data);
        
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
           // laneObject.transform.localPosition = pos;

            laneObject.transform.localPosition = Vector3.Lerp(laneObject.transform.localPosition, pos, _x );
           // laneObject.transform.rigidbody2D.velocity = new Vector2(-20*_x, 0);
           // forwardMoveSpeed -= 0.05f;

            int offsetX;
            if (Data.Instance.GetComponent<WordsData>().GetZone() == 1)
            {
                offsetX = -1438;
            }
            else
            {
                offsetX = -458;
            }
            if (laneObject.repeatIn > 0 && pos.x < offsetX + laneObject.repeatIn)
            {
                pos.x = laneObject.repeatIn;
                laneObject.transform.localPosition = pos;
            } else
                if (pos.x < -1200 && laneObject.repeatIn<1) DeleteObject(laneObject);

        }
    }

}
