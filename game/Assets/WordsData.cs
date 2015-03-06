using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordsData : MonoBehaviour {

    
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
}
