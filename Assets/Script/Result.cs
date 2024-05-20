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
        day.text = "Day : " + currentDay.ToString();
        totalCoin.text = "Total : " + GameManager.instance.totalMoney.ToString();
        todayCoin.text = "Coin : " + GameManager.instance.money.ToString();
        repute.text = $"Reputation : {beforeRepute} -> {GameManager.instance.repute}";
    }

    public void OnClickNext()
    {
        gameObject.SetActive(false);
        //GameManager.instance.isPlaying = true;
        if(!title.gameObject.activeSelf )
        {
            GameManager.instance.money = 0;
            title.gameObject.SetActive(true);
        }
    }

}
