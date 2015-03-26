using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stars : MonoBehaviour {

    public Image star1;
    public Image star2;
    public Image star3;

    public void Init(int stars)
    {
        if (stars == 0)
        {
            SetOff(star1);
            SetOff(star2);
            SetOff(star3);
        }
        if (stars == 1)
        {
            SetOff(star2);
            SetOff(star3);
        }
        else if (stars == 2)
        {
            SetOff(star3);
        }
    }
    void SetOff(Image star)
    {
        star.enabled = false;
    }
}
