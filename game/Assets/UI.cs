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
        Invoke("SayIntro", 1);
    }
    void SayIntro()
    {
        DisplayWord();
    //    return;
      //  Events.OnSoundFX("1_YouHaveToCatchTheWord");
       // Invoke("DisplayWord", 2f);
    }
    void DisplayWord()
    {        
        WordsData.Word word = wordsData.GetWordData();
        Events.OnVoice(word.sightWord);
        if (word != null)
        {
            if (Data.Instance.caps)
                 SightWord.text = word.sightWord.ToUpper();
            else
                SightWord.text = word.sightWord.ToLower();
        }
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
