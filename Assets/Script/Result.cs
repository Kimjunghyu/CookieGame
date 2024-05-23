using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI day;
    public TextMeshProUGUI totalCoin;
    public TextMeshProUGUI todayCoin;
    public TextMeshProUGUI tax;
    public TextMeshProUGUI resultCoin;
    public TextMeshProUGUI repute;

    public GameObject title;
    int beforeRepute = 0;

    private void OnEnable()
    {
        int currentDay = GameManager.instance.day - 1;
        day.text = currentDay.ToString() + $"{TransrationKr.instance.GetTranslation("Day")}";
        totalCoin.text = $"{TransrationKr.instance.GetTranslation("Result1")}" + " : " + GameManager.instance.totalMoney.ToString();
        todayCoin.text = $"{TransrationKr.instance.GetTranslation("Result2")}"+" : " + GameManager.instance.money.ToString();
        tax.text = $"{TransrationKr.instance.GetTranslation("Result3")}" + " : " + GameManager.instance.tax.ToString();
        resultCoin.text = $"{TransrationKr.instance.GetTranslation("Result4")}" + " :" + GameManager.instance.currCoin.ToString();
        repute.text = $"{TransrationKr.instance.GetTranslation("Repute")}" + $" : {beforeRepute} ¢º {GameManager.instance.repute}";
    }

    private void OnDisable()
    {
        GameManager.instance.totalMoney = GameManager.instance.currCoin;
        beforeRepute = GameManager.instance.repute;
    }
    public void OnClickNext()
    {
        gameObject.SetActive(false);
        if(!title.gameObject.activeSelf )
        {
            GameManager.instance.money = 0;
            title.gameObject.SetActive(true);
        }
    }

}
