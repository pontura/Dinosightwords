using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaneObject : MonoBehaviour {

    public LaneObjectData data;

    public void SetData(LaneObjectData data)
    {
        this.data = data;
        if (data.word != "")
        {
            GetComponentInChildren<Text>().text = data.word;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (data.isObstacle)
            {

            }
            else
            {
                Events.OnPlayerHitObject(data);
                gameObject.SetActive(false);
            }
        }
    }
}
