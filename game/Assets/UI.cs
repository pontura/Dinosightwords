using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField]
    PausedMenu pausedMenu;

    public Text scoreLabel;
    public Text SightWord;
    private WordsData wordsData;

   public void Init()
    {
        pausedMenu.gameObject.SetActive(false);

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
        Events.OnVoice(word.sightWord);
        SightWord.text = "";
        Invoke("DisplayWord", 1);
    }
    void DisplayWord()
    {
        WordsData.Word word = wordsData.GetWordData();
        if(word != null)
            SightWord.text = word.sightWord;
    }
    public void OnPauseButton()
    {
        pausedMenu.gameObject.SetActive(true);
        Events.OnGamePaused(true);
    }
}
