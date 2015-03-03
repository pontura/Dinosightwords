﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("SPACE#");
            Events.OnSwipe(SwipeDetector.directions.DOWN);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Events.OnSwipe(SwipeDetector.directions.UP);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Events.OnSwipe(SwipeDetector.directions.DOWN);

        //RaycastHit hit;
        //Ray ray;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = UICamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider != null)
        //        {
        //            Events.OnUIClicked(hit.collider.gameObject);
        //            return;
        //        }
        //    }
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.tag == "Hero")
        //        { }
        //        }

        //}
    }
}
