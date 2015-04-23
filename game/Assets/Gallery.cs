using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gallery : MonoBehaviour {

    public GameObject diploma1;
    public GameObject diploma2;

    public GameObject container;
    [SerializeField] GalleryButton button;
    public int buttonSeparation;
    private WordsData wordsData;
    private float starting_Y ;
    private int containerHeight = 0;
    private float scalable = 0.9f;

    public void MainMenu()
    {
        Application.LoadLevel("02_MainMenu");
    }
	void Start () {
        starting_Y = container.transform.localPosition.y;
        wordsData = Data.Instance.GetComponent<WordsData>();
        UserData userData = Data.Instance.GetComponent<UserData>();

        int b = 0;
        int a = 0;
        foreach (WordsData.Zone zone in wordsData.Zone1)
        {
            int starsInLevel = userData.GetStarsIn( b + 1);
            bool isActive = false;
            int levelToReachWord;
            if (starsInLevel > 0)
            {
                levelToReachWord = 0;
                isActive = true;
            }
            else
                levelToReachWord = b;

            if (b == 16)
            {
                GameObject diploma = Instantiate(diploma1, Vector3.zero, Quaternion.identity) as GameObject;
                diploma.transform.SetParent(container.transform);

                diploma.transform.localPosition = new Vector3(0, -containerHeight, 0);
                diploma.transform.localScale = new Vector3(scalable, scalable, scalable);

                diploma.GetComponent<Button>().onClick.AddListener(delegate() { DiplomaClick(diploma); });
                a++;
                containerHeight += (buttonSeparation * 3);
                diploma.GetComponent<GalleryButton>().id = 1;
                if (Data.Instance.GetComponent<UserData>().diplomaId < 1) diploma.GetComponent<GalleryButton>().isActive = false;
            }
            foreach (WordsData.Word word in zone.words)
            {
                if (word.sightWord.ToUpper() == "RANDOM") continue;

               GalleryButton galleryButton = Instantiate(button) as GalleryButton;
               galleryButton.transform.SetParent(container.transform);

               galleryButton.Init(this, word.sightWord, isActive, levelToReachWord);
               galleryButton.transform.localPosition = new Vector3(0, -containerHeight, 0);
               galleryButton.transform.localScale = new Vector3(scalable, scalable, scalable);

               galleryButton.GetComponent<Button>().onClick.AddListener(delegate() { PlayWord(galleryButton); });
               a++;
               containerHeight += buttonSeparation;
            }
            b++;
        }

        GameObject lastDiploma = Instantiate(diploma2, Vector3.zero, Quaternion.identity) as GameObject;
        lastDiploma.transform.SetParent(container.transform);

        lastDiploma.transform.localPosition = new Vector3(0, -containerHeight, 0);
        lastDiploma.transform.localScale = new Vector3(scalable, scalable, scalable);

        lastDiploma.GetComponent<Button>().onClick.AddListener(delegate() { DiplomaClick(lastDiploma); });
        containerHeight += (buttonSeparation * 3);

        lastDiploma.GetComponent<GalleryButton>().id = 2;
        if (Data.Instance.GetComponent<UserData>().diplomaId < 1) lastDiploma.GetComponent<GalleryButton>().isActive = false;


        containerHeight -= buttonSeparation;
        container.GetComponent<RectTransform>().sizeDelta = new Vector2(600, containerHeight);
	}
    public void DiplomaClick(GameObject dipoloma)
    {
        int id = dipoloma.GetComponent<GalleryButton>().id ;
        bool active = dipoloma.GetComponent<GalleryButton>().isActive;
        if (active)
          GetComponent<Diploma>().Init(id);
    }
    public void PlayWord(GalleryButton button)
    {
        print("PlayWord " + button.isActive + " - " + button.sightWord);
        if(button.isActive)
            Events.OnVoice(button.sightWord);
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
