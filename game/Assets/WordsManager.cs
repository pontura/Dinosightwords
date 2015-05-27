using UnityEngine;
using System.Collections;

public class WordsManager : MonoBehaviour {
    
    private WordsData wordsData;
    private int SightWordProbabilityPercent = 50;

    public LaneObject LaneObject_Word;
    private bool lastWordWasSightword;

    public void Init()
    {
        wordsData = Data.Instance.GetComponent<WordsData>();
       // Events.OnNewWord(wordsData.GetWordData());
    }
    public LaneObject GetNewObject()
    {
        
        LaneObject laneObject = LaneObject_Word;
        
        LaneObjectData data = new LaneObjectData();

        int rand = Random.Range(0, 100);

        if (rand <= SightWordProbabilityPercent && !lastWordWasSightword)
        {
            data.word = CurrentWord().sightWord;
            data.score = 1;
            lastWordWasSightword = true;
        }
        else
        {
            lastWordWasSightword = false;
            string word;
            if (Random.Range(0, 100) < 33) word = CurrentWord().wrong1;
            else if (Random.Range(0, 100) < 66) word = CurrentWord().wrong2;
            else word = CurrentWord().wrong3;

            

            data.word = word;
            data.score = -1;


        }

        if (data.word == null || data.word == "")
        {
            Debug.LogError("No hay words ");
            return null;
        }
        
        if (!passFilter(data.word))
        {
            return GetNewObject();
        }
        laneObject.data = data;

        return laneObject;
    }
    public WordsData.Word CurrentWord()
    {
        return wordsData.GetWordData();
    }

    string lastWord;
    public bool passFilter(string newWord)
    {
        if (newWord == "" || newWord.ToUpper() == "RANDOM") return true;
        if(newWord == lastWord) return false;
        lastWord = newWord;
        return true;
    }
}
