using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    int distance = 100;

	public void MoveUP()
    {
        Move(distance);
    }
    public void MoveDown()
    {
        Move(-distance);
    }
    public void Move(int _y)
    {
        print("move" + _y);
        Vector3 pos = transform.localPosition;
        pos.y += _y;
        transform.localPosition = pos;
    }
}
