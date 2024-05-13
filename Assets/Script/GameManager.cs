using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;

    private int day = 1;
    private int min = 3;
    private float second = 0f;
    public int money = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        min = 3;
        second = 60f;
        timerText.text = string.Format($"{0:D2}:{1:D2}", min, (int)second);
        dayText.text = $"Day : {day}";
        moneyText.text = $"Money: ${money}";
    }
    private void Update()
    {
        timerText.text = $"{min:D2}:{(int)second:D2}";
        second -= Time.deltaTime;
        if (second <= 0)
        {
            min -= 1;
            second = 60;
            if(min <= 0)
            {
                min = 0;
                second = 0;
                //day++; (프로토타입 이후 추가예정)
            }
            Debug.Log(min);
            Debug.Log(second);
        }

    }

    public void OnClickStop()
    {
        Time.timeScale = 0f;
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;

    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = $"Money: ${money}";
    }
}
