using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodayCookieInfo : MonoBehaviour
{
    public Image[] images;
    public Sprite[] cookies;
    public Sprite empty;
    private int count = 0;
    private int currentStage = 0;

    private void OnEnable()
    {
        for(int i = 0; i < images.Length; ++i)
        {
            images[i].sprite = empty;
        }
        if(GameManager.instance.stage != 0)
        {
            currentStage = GameManager.instance.stage;
        }
        else
        {
            currentStage = 1;
        }
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
            //gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        count = 0;
    }
}
