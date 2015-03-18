using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Character : MonoBehaviour {

    [SerializeField]
    Hero heroAsset;

    int distance ;
    float timeToCrossLane;
    public Lane lane;
    public states state;
    private int posX;

    public enum states
    {
        IDLE,
        CHANGE,
        JUMP,
        CRASH
    }
    public void Init()
    {
        distance = Data.Instance.gameData.laneSeparation / 2;
        timeToCrossLane = Data.Instance.gameData.timeToCrossLane / 2;

        Hero hero = Instantiate(heroAsset) as Hero;
        hero.transform.SetParent(transform);
        hero.GetComponent<RectTransform>().localScale = Vector3.one;
        hero.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
    void Start()
    {
        posX = Data.Instance.gameData.CharacterXPosition;
        Vector3 pos = transform.localPosition;
        pos.x = posX;
        transform.localPosition = pos;
    }
	public void MoveUP()
    {
        Vector3 pos = transform.localPosition;
        pos.y += distance;
        Move(pos);
    }
    public void MoveDown()
    {
        Vector3 pos = transform.localPosition;
        pos.y -= distance;
        Move(pos);
    }
    public void GotoCenterOfLane()
    {
        Vector3 pos = transform.localPosition;
        pos.y = 0;
        Move(pos);
    }
    public void Move(Vector3 pos)
    {
        state = states.CHANGE;
        TweenParms parms = new TweenParms();
        parms.Prop("localPosition", pos);
        parms.Ease(EaseType.Linear);
        if(pos.y == 0)
            parms.OnComplete(OnChangeLaneComplete);
        else
            parms.OnComplete(OnChangeingLane);
        HOTween.To(transform, timeToCrossLane, parms);
    }
    void OnChangeingLane()
    {
        Events.OnChangeingLane();
    }
    void OnChangeLaneComplete()
    {
        state = states.IDLE;
        Events.OnChangeLaneComplete();
    }
}
