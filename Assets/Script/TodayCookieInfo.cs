using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodayCookieInfo : MonoBehaviour
{
    public Image[] images;
    public Sprite[] cookies;

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
                images[i].gameObject.SetActive(true);
                images[i].sprite = cookies[i];
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
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
