using UnityEngine;
using System.Collections;

public class LaneObject : MonoBehaviour {

    public void Destroy()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

        }
    }
}
