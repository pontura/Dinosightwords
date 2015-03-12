using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserData : MonoBehaviour {

    public bool DEBUG_UnlockAllLevels;
    public List<int> errorsZone1;
    public List<int> errorsZone2;

    private WordsData wordsData;
    private Data data;

	public void Init () {
        Events.OnLevelComplete += OnLevelComplete;
        wordsData = GetComponent<WordsData>();
        data = GetComponent<Data>();
        LoadData();
	}
    void OnLevelComplete()
    {
        int levelID = wordsData.LevelID;
        int errors = data.errors;
        if (errorsZone1.Count < levelID)
            SaveStars(levelID, errors);
        else if (errorsZone1[levelID-1] > errors)
            SaveStars(levelID, errors);
    }
    void SaveStars(int levelID, int errors)
    {
        int stars;
        if (errors < 2) stars = 3;
        else if (errors < 4) stars = 2;
        else stars = 1;
        PlayerPrefs.SetInt("level_1_" + levelID, stars);   
    }
    void LoadData()
    {
        int levelID = 0;
        foreach (WordsData.Zone zone  in wordsData.Zone1)
        {
            levelID++;
            //foreach (WordsData.Word word in zone.words)
            //{
                print(zone.words);
                LoadStars(1, levelID);
           // }
        }
    }
    void LoadStars(int ZoneID, int levelID)
    {
        print("level_" + ZoneID + "_" + levelID + " - PlayerPrefs " + PlayerPrefs.GetInt("level_" + ZoneID + "_" + levelID) );
        if ( PlayerPrefs.GetInt("level_" + ZoneID + "_" + levelID)>0)
        {
            int levelStars = PlayerPrefs.GetInt("level_" + ZoneID + "_" + levelID);
            errorsZone1.Add(levelStars);
        }
    }
    public int GetStarsIn(int ZoneID, int LevelID)
    {
        if (errorsZone1.Count < LevelID ) 
            return 0;
        int stars = errorsZone1[LevelID - 1];

        return stars;
    }
}
