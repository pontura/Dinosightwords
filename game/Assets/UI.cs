using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField]
    PausedMenu pausedMenu;

    [SerializeField]
    ScoreProgress scoreProgress;

    public Text SightWord;
    private WordsData wordsData;

   public void Init()
    {
        SightWord.text = "";
        pausedMenu.gameObject.SetActive(false);
        
        Events.OnNewWord += OnNewWord;
        wordsData = Data.Instance.GetComponent<WordsData>();

        scoreProgress.Init(wordsData.GetScoreCurrentLevel());
    }
    void OnDestroy()
    {
        Events.OnNewWord -= OnNewWord;
    }
    void OnNewWord(WordsData.Word word)
    {
        print("on new word");
        SightWord.text = "";
        Invoke("DisplayWord", 1);
    }
    void DisplayWord()
    {
        
        WordsData.Word word = wordsData.GetWordData();
        Events.OnVoice(word.sightWord);
        if(word != null)
            SightWord.text = word.sightWord;
    }
    public void OnPauseButton()
    {
        pausedMenu.gameObject.SetActive(true);
        Events.OnGamePaused(true);
    }
}
