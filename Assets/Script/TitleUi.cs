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
    public Slider volumeValue;
    public Toggle volumeToggle;
    private void Start()
    {
        titleDay.text = $"Day : 1";
        titleGold.text = GameManager.instance.totalMoney.ToString();
    }

    private void OnEnable()
    {
        if(GameManager.instance != null)
        {
            titleDay.text = $"Day : {GameManager.instance.day}";
            titleGold.text = GameManager.instance.totalMoney.ToString();
        }

    }

    private void Update()
    {
        if (option.gameObject.activeSelf)
        {
            GameManager.instance.bgmValue = volumeValue.value;
            if(volumeToggle.isOn)
            {
                GameManager.instance.bgmPlaying = true;
            }
            else
            {
                GameManager.instance.bgmPlaying=false;
            }
        }
    }
    public void OnClickShop()
    {
        if (!shop.gameObject.activeSelf)
        {
            shop.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void OnClickOption()
    {
        if(!option.gameObject.activeSelf)
        {
            option.gameObject.SetActive(true);
            volumeValue.value = GameManager.instance.bgmValue;
        }
    }

    public void OnClickOptionQuit()
    {
        if(option.gameObject.activeSelf)
        {
            option.gameObject.SetActive(false);
        }
    }
}
