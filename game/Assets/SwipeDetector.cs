using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{
    public float minSwipeDistY;
    public float minSwipeDistX;
    private Vector2 startPos;

    public enum directions
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    private float newTime;
    private bool touched;

    void Update()
	{
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches[0];
			switch (touch.phase) 
			{
			    case TouchPhase.Began:
                    newTime = 0;
                    touched = true;
				    startPos = touch.position;				
				    break;					
			    case TouchPhase.Ended:
                    newTime = 0;
                    touched = false;
				    break;
			}



            if (touched)
                newTime += Time.deltaTime;

            if (newTime > 0.06f && touched)
            {
                Move(touch.position.y);
            }

            if (newTime > 1)
            {
                touched = true;
            }


		}
	}
    void Move(float touchFinalPositionY)
    {
        float swipeDistVertical = (new Vector3(0, touchFinalPositionY, 0) - new Vector3(0, startPos.y, 0)).magnitude;
      //  float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
        if (swipeDistVertical > minSwipeDistY)
        {
            float swipeValue = Mathf.Sign(touchFinalPositionY - startPos.y);
            if (swipeValue > 0)
                Swipe(directions.UP);
            else if (swipeValue < 0)
                Swipe(directions.DOWN);
        }
        else
            //if (swipeDistHorizontal > minSwipeDistX)
            //{
            //    float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
            //    if (swipeValue > 0)
            //        Swipe(directions.RIGHT);
            //    else if (swipeValue < 0)
            //        Swipe(directions.LEFT);
            //}
            //else
                Events.OnHeroJump();
    }
    void Swipe(directions direction)
    {
        print(direction);

        switch (direction)
        {
            case directions.UP:
                Events.OnSwipe(directions.UP); break;
            case directions.DOWN:
                Events.OnSwipe(directions.DOWN); break;
        }
    }
}