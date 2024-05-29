using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUi : MonoBehaviour
{
    public GameObject title;

    public Sprite[] step1;
    public Sprite[] step2;
    public Sprite step3;
    public Sprite step4;
    public Sprite[] step5;
    public Image uiImage;
    private int count = 0;
    private int stepCount = 0;
    private int finalCount = 0;
    private bool finalStep = false;
    private void OnEnable()
    {
        count = 0;
        stepCount = 0;
        finalCount = 0;
        finalStep = false;
        uiImage.sprite = step1[0];
    }
    private void Update()
    {
        if(count < 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                count++;
                if (count <= 2)
                {
                    uiImage.sprite = step1[count];
                }
                else if (count == 3)
                {
                    uiImage.sprite = step2[stepCount];
                }
            }
        }
        if(uiImage.sprite == step3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                uiImage.sprite = step4;
            }
        }
        if(finalStep)
        {
            if (Input.GetMouseButtonDown(0))
            {
                finalCount++;
                if (finalCount >= step5.Length)
                {
                    OnClickSkip();
                }
                else
                {
                    uiImage.sprite = step5[finalCount];
                }
            }
        }

    }

    public void OnClickStep2Button1()
    {
        if(uiImage.sprite == step2[0])
        {
            stepCount++;
            uiImage.sprite = step2[stepCount];
        }
        else
        {
            return;
        }
    }
    public void OnClickStep2Button2()
    {
        if (uiImage.sprite == step2[1])
        {
            stepCount++;
            uiImage.sprite = step2[stepCount];
        }
        else
        {
            return;
        }
    }
    public void OnClickStep2Button3()
    {
        if (uiImage.sprite == step2[2])
        {
            stepCount++;
            uiImage.sprite = step2[stepCount];
        }
        else
        {
            return;
        }
    }

    public void OnClickStep2Button4()
    {
        if (uiImage.sprite == step2[3])
        {
            uiImage.sprite = step3;
        }
        else
        {
            return;
        }
    }

    public void OnClickStep2Button9()
    {
        if (uiImage.sprite == step4)
        {
            finalStep = true;
            uiImage.sprite = step5[0];
        }
        else
        {
            return;
        }
    }

    public void OnClickSkip()
    {
        gameObject.SetActive(false);
        if(!title.gameObject.activeSelf)
        {
            title.gameObject.SetActive(true);
        }
    }

    private void tutorialComplete()
    {
        PlayerPrefs.SetInt("tutorialPlay", 1);
        PlayerPrefs.Save();
    }
}
