using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text scoreLabel;
    private int score = 0;

    void Start()
    {
        Events.OnPlayerHitObject += OnPlayerHitObject;
    }
    void OnDestroy()
    {
        Events.OnPlayerHitObject -= OnPlayerHitObject;
    }
    void OnPlayerHitObject(LaneObjectData data)
    {
        score += data.score;
        scoreLabel.text = "SCORE = " + score.ToString();
    }
}
