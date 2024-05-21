using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleUi : MonoBehaviour
{
    public GameObject shop;

    public TextMeshProUGUI titleDay;
    public TextMeshProUGUI titleGold;

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

    public void OnClickShop()
    {
        if (!shop.gameObject.activeSelf)
        {
            shop.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
