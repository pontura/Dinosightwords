using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gallery : MonoBehaviour {

    public GameObject container;
    [SerializeField] GalleryButton button;
    public int buttonSeparation;
    private WordsData wordsData;
    private float starting_Y ;
    private int containerHeight = 0;

    public void MainMenu()
    {
        Application.LoadLevel("02_MainMenu");
    }
	void Start () {
        starting_Y = container.transform.localPosition.y;
        wordsData = Data.Instance.GetComponent<WordsData>();
        UserData userData = Data.Instance.GetComponent<UserData>();
        int a = 0;
        
        foreach (WordsData.Zone zone in wordsData.Zone1)
        {
            foreach (WordsData.Word word in zone.words)
            {
                GalleryButton galleryButton = Instantiate(button) as GalleryButton;
               galleryButton.transform.SetParent(container.transform);

               int levelToReachWord;
               int starsInLevel = userData.GetStarsIn(1, a+1);

               if (starsInLevel > 0)
                   levelToReachWord = 0;
               else
                   levelToReachWord = a;

               galleryButton.Init(this, word.sightWord, levelToReachWord);
               galleryButton.transform.localPosition = new Vector3(0, -buttonSeparation * a, 0);
               galleryButton.transform.localScale = Vector3.one;

               galleryButton.GetComponent<Button>().onClick.AddListener(delegate() { PlayWord(galleryButton); });
               a++;
               containerHeight += buttonSeparation;
            }
        }
        containerHeight -= buttonSeparation;
        container.GetComponent<RectTransform>().sizeDelta = new Vector2(600, containerHeight);
	}
    public void PlayWord(GalleryButton button)
    {
        if(button.isActive)
            print("play " + button.sightWord);
    }
    void Update()
    {
        Vector3 pos = container.transform.localPosition;
        if (pos.y < starting_Y)
            pos.y = starting_Y;
        else if (pos.y > containerHeight - starting_Y)
            pos.y = containerHeight - starting_Y;
        container.transform.localPosition = pos;
    }
}
