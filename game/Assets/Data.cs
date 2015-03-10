using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    private string movPath = "bumper04.mp4";

    static Data mInstance = null;

    public int totalScore;

    public float musicVolume = 1;
    public float soundsVolume = 1;

    public SceneLoader sceneLoader;
    public GameData gameData;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                Debug.LogError("Algo llama a DATA antes de inicializarse");
            }
            return mInstance;
        }
    }
    void Awake()
    {
        mInstance = this;
        DontDestroyOnLoad(this);

        gameData = GetComponent<GameData>();
        GetComponent<WordsData>().Init(1,1,1);

//        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
//        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;
//        Events.OnSaveVolumes += OnSaveVolumes;

//#if UNITY_ANDROID || UNITY_IPHONE
//        Handheld.PlayFullScreenMovie(movPath, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
//#endif

    }
    void Start()
    {
        Events.OnSceneLoad("Main");
    }
    void OnMusicVolumeChanged(float value)
    {
        musicVolume = value;
    }
    void OnSoundsVolumeChanged(float value)
    {
        soundsVolume = value;
    }
    void OnBadgeSelected(int _totalScore)
    {
        this.totalScore = _totalScore;
    }
    public void Reset()
    {
    }
    void OnSaveVolumes(float _musicVolume, float _soundsVolume)
    {
        this.musicVolume = _musicVolume;
        this.soundsVolume = _soundsVolume;
    }
}
