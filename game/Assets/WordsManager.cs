using UnityEngine;
using System.Collections;

public class WordsManager : MonoBehaviour {
    
    private WordsData wordsData;
    private int SightWordProbabilityPercent = 50;

    public LaneObject LaneObject_Word;

    private int wordScore=0;

    public void Init()
    {
        wordsData = Data.Instance.GetComponent<WordsData>();
        Events.OnPlayerHitObject += OnPlayerHitObject;
        wordScore = 0;
    }
    void OnDestroy()
    {
        Events.OnPlayerHitObject -= OnPlayerHitObject;
    }
    void OnPlayerHitObject(LaneObjectData data)
    {
        if (data.score <= 0) return;
        wordScore++;
        if (wordScore >= CurrentWord().score)
        {
            Events.SetNextWord();
            wordScore = 0;
        }
    }
    public LaneObject GetNewObject()
    {
        LaneObject laneObject = LaneObject_Word;
        
        LaneObjectData data = new LaneObjectData();
        if (Random.Range(0, 100) < SightWordProbabilityPercent)
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
        if(newWord == lastWord) return false;
        lastWord = newWord;
        return true;
    }
}
