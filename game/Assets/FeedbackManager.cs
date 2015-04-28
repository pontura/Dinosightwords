using UnityEngine;
using System.Collections;

public class FeedbackManager : MonoBehaviour {

    public GameObject feedbackSignal;

    [SerializeField]
    GameObject container;

	void Start () {
        Events.OnPlayerHitWord += OnPlayerHitWord;
	}

    void OnDestroy()
    {
        Events.OnPlayerHitWord -= OnPlayerHitWord;
    }

    void OnPlayerHitWord(LaneObjectData data)
    {
        feedbackSignal.transform.SetParent(container.transform);
        feedbackSignal.transform.localScale = Vector3.one;
        feedbackSignal.transform.localPosition = Vector3.zero;
        bool isOk = false;
        if (data.score > 0) isOk = true;
        feedbackSignal.GetComponent<FeedbackSignal>().Init(isOk);
    }
}
