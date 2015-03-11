using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    private string movPath = "bumper04.mp4";
    
    public int totalScore;

    public float musicVolume = 1;
    public float soundsVolume = 1;

    public SceneLoader sceneLoader;
    public GameData gameData;


    const string PREFAB_PATH = "Data";

    static Data mInstance = null;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                }
            }
            return mInstance;
        }
    }

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!mInstance)
            mInstance = this;
        //otherwise, if we do, kill this thing
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

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
