using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleUi : MonoBehaviour
{
    public GameObject shop;
    public GameObject option;
    public TextMeshProUGUI titleDay;
    public TextMeshProUGUI titleGold;
    public TextMeshProUGUI titleRepute;
    public Slider volumeValue;
    public Toggle volumeToggle;
    public Toggle effectSoundToggle;

    private int testCount = 0;
    private void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadGameData();
            titleDay.text = $"{GameManager.instance.day}";
            titleGold.text = GameManager.instance.totalMoney.ToString();
            titleRepute.text = GameManager.instance.repute.ToString();
        }
    }

    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            titleDay.text = $"{GameManager.instance.day}";
            titleGold.text = GameManager.instance.totalMoney.ToString();
            titleRepute.text = GameManager.instance.repute.ToString();
        }
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (option.activeSelf)
                {
                    option.SetActive(false);
                }
                else
                {
                    OnClickOption();
                }
            }
        }

        if (option.activeSelf)
        {
            GameManager.instance.bgmValue = volumeValue.value;
            GameManager.instance.bgmPlaying = volumeToggle.isOn;
        }

        if(effectSoundToggle.isOn)
        {
            GameManager.instance.soundEffect = true;
        }
        else
        {
            GameManager.instance.soundEffect = false;
        }
    }

    public void OnClickShop()
    {
        if (!shop.activeSelf)
        {
            shop.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void OnClickOption()
    {
        if (!option.activeSelf)
        {
            option.SetActive(true);
            volumeValue.value = GameManager.instance.bgmValue;
        }
    }

    public void OnClickOptionQuit()
    {
        if (option.activeSelf)
        {
            testCount = 0;
            option.SetActive(false);
        }
    }

    public void OnClickTestCount()
    {
        testCount++;
        if(testCount > 4)
        {   
            GameManager.instance.ResetPlayerPrefs();
            GameManager.instance.OnClickGameExit();

        }
    }
}
