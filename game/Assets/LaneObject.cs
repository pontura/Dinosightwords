﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaneObject : MonoBehaviour {

    public LaneObjectData data;
    public int repeatIn;
    public bool slide;

    public void SetData(LaneObjectData data)
    {
        this.data = data;
        if (tag == "Word")
        {
            GetComponentInChildren<Text>().text = data.word.ToUpper();
        }
       // transform.rigidbody2D.velocity = new Vector2(-200, 0);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (slide)
            {
                Events.OnHeroSlide();
            }else
            if (tag == "Obstacle")
            {
                Events.OnHeroCrash();
            }
            else
            {
                Events.OnPlayerHitWord(data);
                gameObject.SetActive(false);
            }
        }
        GetComponent<Collider2D>().enabled = false;
    }
}
