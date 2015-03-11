using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text scoreLabel;
    public Text SightWord;
    private WordsData wordsData;

   public void Init()
    {
        Events.OnScoreRefresh += OnScoreRefresh;
        Events.OnNewWord += OnNewWord;
        wordsData = Data.Instance.GetComponent<WordsData>();
        DisplayWord();
    }
    void OnDestroy()
    {
        Events.OnNewWord -= OnNewWord;
        Events.OnScoreRefresh -= OnScoreRefresh;
    }
    void OnScoreRefresh(int score)
    {        
        scoreLabel.text = "SCORE = " + score.ToString();
    }
    void OnNewWord(WordsData.Word word)
    {
        SightWord.text = "";
        Invoke("DisplayWord", 1);
    }
    void DisplayWord()
    {
        SightWord.text = wordsData.GetWordData().sightWord;
    }
}
