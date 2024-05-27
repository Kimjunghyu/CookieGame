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

    private void Start()
    {
        titleDay.text = "1" + $"{TransrationKr.instance.GetTranslation("Day")}";
        titleGold.text = GameManager.instance.totalMoney.ToString();
        titleRepute.text = $"{TransrationKr.instance.GetTranslation("Repute")}" + " : 0";
    }

    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            titleDay.text = $"{GameManager.instance.day}" + $"{TransrationKr.instance.GetTranslation("Day")}";
            titleGold.text = GameManager.instance.totalMoney.ToString();
            titleRepute.text = $"{TransrationKr.instance.GetTranslation("Repute")}" + " : " + GameManager.instance.repute.ToString();
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
            option.SetActive(false);
        }
    }
}
