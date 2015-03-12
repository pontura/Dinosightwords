using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordsData : MonoBehaviour {

    public int ZoneID;
    public int LevelID;
    public int WordID;

    private Zone[] actualZone;

    public int nextScore;

    public void Init(int ZoneID, int LevelID, int WordID)
    {
        SetZone(ZoneID);
        this.ZoneID = ZoneID;
        this.LevelID = LevelID;
        this.WordID = WordID;
        Events.SetNextWord += SetNextWord;
    }
    public void Restart()
    {
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
    public Zone[] Zone2;

    public void SetNextWord()
    {
        WordID++;
        ResetRandomWord();
        if (actualZone[LevelID - 1].words.Length < WordID)
        {
            Events.OnLevelComplete();
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
            Word word = actualZone[LevelID - 1].words[WordID - 1];
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
    void SetZone(int id)
    {
        switch (id)
        {
            case 1: actualZone = Zone1; break;
            default: actualZone = Zone2; break;
        }
    }
    Word randomWord;
    string lastRandomWord = "";
    void ResetRandomWord()
    {
        randomWord = null;
    }
    void SetRandomWord()
    {
        int rand = UnityEngine.Random.Range(0, LevelID);
        Zone zone = actualZone[rand];

        rand = UnityEngine.Random.Range(0, zone.words.Length);
        randomWord = zone.words[rand];

        if (randomWord.sightWord.ToUpper() == "RANDOM") SetRandomWord();
        if (lastRandomWord == randomWord.sightWord) SetRandomWord();
    }
}
