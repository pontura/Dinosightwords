using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField]
    PausedMenu pausedMenu;

    [SerializeField]
    ScoreProgress scoreProgress;

    [SerializeField]
    GameObject menuButton;

    public Text SightWord;
    private WordsData wordsData;

   public void Init()
    {
        SightWord.text = "";
        
        Events.OnNewWord += OnNewWord;
        Events.OnLevelComplete += OnLevelComplete;

        wordsData = Data.Instance.GetComponent<WordsData>();

        scoreProgress.Init(wordsData.GetScoreCurrentLevel());
    }
    void OnDestroy()
    {
        Events.OnNewWord -= OnNewWord;
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void OnNewWord(WordsData.Word word)
    {
        SightWord.text = "";
        Invoke("DisplayWord", 1);
    }
    void DisplayWord()
    {        
        WordsData.Word word = wordsData.GetWordData();
        Events.OnVoice(word.sightWord);
        if(word != null)
            SightWord.text = word.sightWord.ToUpper();
    }
    public void OnPauseButton()
    {
        GetComponent<PausedMenu>().Init();
        Events.OnGamePaused(true);
    }
    void OnLevelComplete()
    {
        SightWord.gameObject.SetActive(false);
        scoreProgress.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }
}
