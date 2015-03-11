using UnityEngine;
using System.Collections;

public class Summary : MonoBehaviour {

    public void Next()
    {
        WordsData data = Data.Instance.GetComponent<WordsData>();
        data.LevelID++;
        data.WordID = 1;
        Application.LoadLevel("Game");
    }
    public void RePlay()
    {
        Application.LoadLevel("Game");
    }
}
