using UnityEngine;
using System.Collections;

public class WordsManager : MonoBehaviour {
    
    private WordsData wordsData;
    private int SightWordProbabilityPercent = 50;

    public LaneObject LaneObject_Word;

    public void Init()
    {
        wordsData = Data.Instance.GetComponent<WordsData>();  
    }
    public void OnPlayerHitWord(int totalScore)
    {
        if (totalScore >= wordsData.nextScore)
        {
            Events.SetNextWord();
        }
    }
    public LaneObject GetNewObject()
    {
        
        LaneObject laneObject = LaneObject_Word;
        
        LaneObjectData data = new LaneObjectData();

        int rand = Random.Range(0, 100);

        if (rand <= SightWordProbabilityPercent)
        {
            data.word = CurrentWord().sightWord;
            data.score = 1;
        }
        else
        {
            string word;
            if (Random.Range(0, 100) < 33) word = CurrentWord().wrong1;
            else if (Random.Range(0, 100) < 66) word = CurrentWord().wrong2;
            else word = CurrentWord().wrong3;

            data.word = word;
            data.score = -1;
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
        print("passFilter newWord: " + newWord + "      lastWord: " + lastWord);
        if(newWord == lastWord) return false;
        lastWord = newWord;
        return true;
    }
}
