using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleUi : MonoBehaviour
{
    public TextMeshProUGUI titleDay;
    public TextMeshProUGUI titleGold;

    private void Start()
    {
        titleDay.text = $"Day : {GameManager.instance.day.ToString()}";
        titleGold.text = GameManager.instance.totalMoney.ToString();
    }

    private void OnEnable()
    {
        if(GameManager.instance != null)
        {
            titleDay.text = $"Day : {GameManager.instance.day.ToString()}";
            titleGold.text = GameManager.instance.totalMoney.ToString();
        }
        
    }
}
