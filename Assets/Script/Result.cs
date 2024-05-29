using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI totalCoin;
    public TextMeshProUGUI todayCoin;
    public TextMeshProUGUI tax;
    public TextMeshProUGUI resultCoin;
    public TextMeshProUGUI repute;

    public GameObject title;
    public GameObject cookieInfo;
    public GameObject ending;
    int beforeRepute = 0;

    private void OnEnable()
    {
        totalCoin.text = $"{TransrationKr.instance.GetTranslation("Result1")}" + " : " + GameManager.instance.resultTotalMoney.ToString();
        todayCoin.text = $"{TransrationKr.instance.GetTranslation("Result2")}"+" : " + GameManager.instance.money.ToString();
        tax.text = $"{TransrationKr.instance.GetTranslation("Result3")}" + " : " + GameManager.instance.tax.ToString();
        resultCoin.text = $"{TransrationKr.instance.GetTranslation("Result4")}" + " :" + GameManager.instance.currCoin.ToString();
        repute.text = $"{TransrationKr.instance.GetTranslation("Repute")}" + $" : {beforeRepute} ¢º {GameManager.instance.repute}";
    }

    private void OnDisable()
    {
        GameManager.instance.totalMoney = GameManager.instance.currCoin;
        GameManager.instance.resultTotalMoney = GameManager.instance.currCoin;
        beforeRepute = GameManager.instance.repute;
        if (GameManager.instance.resultTotalMoney > 25000)
        {
            ending.gameObject.SetActive(true);
        }
        else
        {
            title.gameObject.SetActive(true);
        }
    }
    public void OnClickNext()
    {
        GameManager.instance.money = 0;
        gameObject.SetActive(false);
    }

}
