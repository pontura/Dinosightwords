using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaneObject : MonoBehaviour {

    public LaneObjectData data;
    public int repeatIn;

    public void SetData(LaneObjectData data)
    {
        this.data = data;
        if (tag == "Word")
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
            if (tag == "Obstacle")
            {
                Events.OnPlayerHitObject(data);
            }
            else
            {
                Events.OnPlayerHitObject(data);
                gameObject.SetActive(false);
            }
        }
        GetComponent<Collider2D>().enabled = false;
    }
}
