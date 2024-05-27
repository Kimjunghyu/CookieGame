using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodayCookieInfo : MonoBehaviour
{
    public Image[] images;
    public Sprite[] cookies;
    private int count = 0;

    private void OnEnable()
    {
        int currentStage = GameManager.instance.stage;
        StageData stageData = StageDataLoad.instance.GetStageData(currentStage - 1);
        if (stageData != null)
        {
            int cookieStart = stageData.cookieStart;
            int cookieEnd = stageData.cookieEnd;
            for(int i = cookieStart; i <= cookieEnd; i++)
            {
                if (!images[count].gameObject.activeSelf)
                {
                    images[count].gameObject.SetActive(true);
                }
                images[count].sprite = cookies[i];
                count++;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        count = 0;
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            gameObject.SetActive(false);
        }
        if(Input.GetMouseButton(0))
        {
            gameObject.SetActive(false);
        }
    }
}
