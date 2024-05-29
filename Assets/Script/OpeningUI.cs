using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpeningUI : MonoBehaviour
{
    public Image image;
    public Sprite[] opImage;
    public Button button;
    public GameObject tutorial;
    public GameObject title;
    private int count = 0;
    
    private void OnEnable()
    {
        if(button.gameObject.activeSelf)
        {
            button.gameObject.SetActive(false);
        }
        if(tutorial.gameObject.activeSelf)
        {
            tutorial.gameObject.SetActive(false);
        }
        if(title.gameObject.activeSelf)
        {
            title.gameObject.SetActive(false);
        }
        count = 0;
        StartCoroutine(StartOp());
    }

    private IEnumerator StartOp()
    {
        while (count < opImage.Length)
        {
            image.sprite = opImage[count++];
            yield return new WaitForSeconds(3);
        }
        if(!button.gameObject.activeSelf)
        {
            button.gameObject.SetActive(true);
        }
        OnFadeOutComplete();
    }

    public void OnClickQuit()
    {
        StartCoroutine(FadeOutAndDisable());
    }

    private IEnumerator FadeOutAndDisable()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
        OnFadeOutComplete();
        if (!PlayerPrefs.HasKey("tutorialPlay") || PlayerPrefs.GetInt("tutorialPlay") == 0)
        {
            if(!tutorial.gameObject.activeSelf)
            {
                tutorial.gameObject.SetActive(true);
            }
        }
        else
        {
            if(!title.gameObject.activeSelf)
            {
                title.gameObject.SetActive(true);
            }
        }
    }

    private void OnFadeOutComplete()
    {
        PlayerPrefs.SetInt("openingShown", 1);
        PlayerPrefs.Save();
    }
}
