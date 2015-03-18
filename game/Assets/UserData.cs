using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserData : MonoBehaviour {

    public bool DEBUG_UnlockAllLevels;
    public List<int> starsZone1;

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
        int stars = ErorsToStars(data.errors);
        if (starsZone1.Count < levelID)
            SaveStars(levelID, stars);
        else if (starsZone1[levelID - 1] > stars)
            SaveStars(levelID, stars);
    }
    public int ErorsToStars(int errors)
    {
        int stars;

        if (errors < 2) stars = 3;
        else if (errors < 4) stars = 2;
        else stars = 1;

        return stars;
    }
    void SaveStars(int levelID, int errors)
    {
        PlayerPrefs.SetInt("level_1_" + levelID, ErorsToStars(errors));   
    }
    void LoadData()
    {
        int levelID = 0;
        foreach (WordsData.Zone zone  in wordsData.Zone1)
        {
            levelID++;
            LoadStars(1, levelID);
        }
    }
    void LoadStars(int ZoneID, int levelID)
    {
        if ( PlayerPrefs.GetInt("level_" + ZoneID + "_" + levelID)>0)
        {
            int levelStars = PlayerPrefs.GetInt("level_" + ZoneID + "_" + levelID);

            if (DEBUG_UnlockAllLevels) 
                levelStars = 3;
            starsZone1.Add(levelStars);
        }
    }
    public int GetStarsIn(int LevelID)
    {
        if (DEBUG_UnlockAllLevels)
            return 3;

       // if (starsZone1.Count < LevelID) 
         //   return 0;
        int stars;

        if (starsZone1.Count < LevelID) 
            return 0;

        stars = starsZone1[LevelID - 1];

        return stars;
    }
}
