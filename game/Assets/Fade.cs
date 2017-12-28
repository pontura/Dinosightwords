using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private Image masker;
    private string m_LevelName = "";
    private int m_LevelIndex = 0;
    private GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        DontDestroyOnLoad(this);
		masker = GetComponentInChildren<Image>();
        masker.enabled = true;
		masker.color = new Color(0,0,0,0);
        graphicRaycaster.enabled = false;
	}

    private IEnumerator FadeStart(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        graphicRaycaster.enabled = true;
        float t = 0;
		while (t < 1)
		{
			yield return new WaitForEndOfFrame();
			t+=Time.deltaTime;
			masker.color = new Color(0,0,0,t);
		}

        if (m_LevelName != "")
			SceneManager.LoadScene(m_LevelName);
        else
			SceneManager.LoadScene(m_LevelIndex);     
		while (t > 0f)
		{
			yield return new WaitForEndOfFrame();
			t-=Time.deltaTime;
			masker.color = new Color(0,0,0,t);
		}
        graphicRaycaster.enabled = false;
    }
    private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        StartCoroutine(FadeStart(aFadeOutTime, aFadeInTime, aColor));
    }

    public void LoadLevel(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        //if (Fading) return;
        m_LevelName = aLevelName;
        StartFade(aFadeOutTime, aFadeInTime, aColor);
    }
}