using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordsData : MonoBehaviour {

    public int LevelID;
    public int WordID;

    public int nextScore;

    public void Init( int LevelID, int WordID)
    {
        this.LevelID = LevelID;
        this.WordID = WordID;
        Events.SetNextWord += SetNextWord;
    }
    public void Restart()
    {
        print("Restart");
        WordID = 1;
        nextScore = 0;
        RefreshNextScore();
    }
    public void RefreshNextScore()
    {
        nextScore += GetWordData().score;
    }
    [Serializable]
    public class Word
    {
        [SerializeField]
        public string sightWord;

        public string wrong1;
        public string wrong2;
        public string wrong3;

        public int score;
    }

    [Serializable]
    public class Zone
    {
        [SerializeField]
        public string title;
        public Word[] words;
    }

    public Zone[] Zone1;

    public void SetNextWord()
    {
        WordID++;
        ResetRandomWord();
        
        if (Zone1[LevelID - 1].words.Length < WordID)
        {
            return;
        }
        else
        {
            RefreshNextScore();
            Events.OnNewWord(GetWordData());
        }
    }
    public Word GetWordData()
    {
        try
        {
            Word word = Zone1[LevelID - 1].words[WordID - 1];
            if (word.sightWord.ToUpper() == "RANDOM" )
            {
                if( randomWord == null)
                    SetRandomWord();
                word = randomWord;
            }
            lastRandomWord = word.sightWord;
            return word;  
        }
        catch
        {
            Debug.LogError("ERROR - no existe el nivel " + LevelID + " o la palabra " + WordID);
            Time.timeScale = 0;
        }
        return null;
    }

    Word randomWord;
    string lastRandomWord = "";
    void ResetRandomWord()
    {
        randomWord = null;
    }
    void SetRandomWord()
    {
        int rand = UnityEngine.Random.Range(0, LevelID-1);
        Zone zone = Zone1[rand];

        rand = UnityEngine.Random.Range(0, zone.words.Length);
        randomWord = zone.words[rand];

        if (randomWord.sightWord.ToUpper() == "RANDOM") SetRandomWord();
        if (lastRandomWord == randomWord.sightWord) SetRandomWord();
    }
    public int GetScoreCurrentLevel()
    {
        int total = 0;
        Word[] words = Zone1[LevelID - 1].words;
        foreach (Word word in words)
        {
            total += word.score;
        }
        return total;
    }
    public int GetZone()
    {
        if (LevelID > 15)
            return 2;
        else return 1;
    }
}
