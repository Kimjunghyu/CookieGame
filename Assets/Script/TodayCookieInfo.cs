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
    private int day = 0;

    private void OnEnable()
    {
        for(int i = 0; i < images.Length; ++i)
        {
            images[i].sprite = empty;
        }
        if (PlayerPrefs.HasKey("day"))
        {
            day = PlayerPrefs.GetInt("day") + 1;
        }
        else
        {
            day = 1;
        }

        GameManager.instance.SetStageTimer(day);
        currentStage = GameManager.instance.stage;

        StageData stageData = StageDataLoad.instance.GetStageData(currentStage-1);
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
    }

    private void OnDisable()
    {
        count = 0;
    }
}
