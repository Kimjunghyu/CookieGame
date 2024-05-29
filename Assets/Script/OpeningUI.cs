using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpeningUI : MonoBehaviour
{
    public Image image;
    public Sprite[] opImage;
    public CanvasGroup canvasGroup;
    public Button button;
    private int count = 0;

    private void OnEnable()
    {
        button.gameObject.SetActive(false);
        count = 0;
        canvasGroup.alpha = 1f;
        StartCoroutine(StartOp());
    }

    private IEnumerator StartOp()
    {
        while (count < opImage.Length)
        {
            image.sprite = opImage[count++];
            yield return new WaitForSeconds(2);
        }
        button.gameObject.SetActive(true);
    }

    public void OnClickQuit()
    {
        StartCoroutine(FadeOutAndDisable());
    }

    private IEnumerator FadeOutAndDisable()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
