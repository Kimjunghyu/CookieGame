using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUi : MonoBehaviour
{
    public GameObject result;
    public GameObject title;
    public Sprite endingStart;
    public Sprite endingSprite;
    public Image ending;
    public Button button;

    private void OnEnable()
    {
        if(button.gameObject.activeSelf)
        {
            button.gameObject.SetActive(false);
        }
        StartCoroutine(StartEnding());
        ending.sprite= endingStart;
    }

    private IEnumerator StartEnding()
    {
        yield return new WaitForSeconds(2);
        ending.sprite = endingSprite;
        button.gameObject.SetActive(true);
    }

    public void OnClickQuit()
    {
        GameManager.instance.ResetPlayerPrefs();
        if(result.gameObject.activeSelf)
        {
            result.gameObject.SetActive(false);
        }
        if(!title.gameObject.activeSelf)
        {
            title.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
