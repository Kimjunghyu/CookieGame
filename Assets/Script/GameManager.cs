using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor.Build;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pause;
    public Button stopButton;
    public GameObject title;
    public GameObject inGame;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;

    private int day = 1;
    private int min = 3;
    private float second = 0f;
    public int money = 0;

    public bool gameOver = false;
    private void Awake()
    {
        Time.timeScale = 0f;
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
            if (min <= 0)
            {
                min = 0;
                second = 0;
                //day++; (프로토타입 이후 추가예정)
                gameOver = true;
            }
        }
        if(gameOver)
        {
            Reset();
            
        }
        
    }

    public void OnClickStop()
    {
        if(!pause.activeSelf)
        {
            Time.timeScale = 0f;
            pause.SetActive(true);
            stopButton.interactable = false;

        }
    }

    public void OnClickRestart()
    {
        if(pause.activeSelf)
        {
            Time.timeScale = 1f;
            pause.SetActive(false);
            stopButton.interactable = true;
        }
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
      //  UnityEditor.EditorApplication.isPlaying = false;
        if(!title.activeSelf)
        {
            title.SetActive(true);
            pause.SetActive(false);
            inGame.SetActive(false);

            gameOver = true;
        }
#else
        Application.Quit();
#endif

    }
    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = $"Money: ${money}";
    }

    public void OnClickStart()
    {
        if (!inGame.activeSelf)
        {
            inGame.SetActive(true);
            Time.timeScale = 1f;
            if (title.activeSelf)
            {
                gameOver = false;
                title.SetActive(false);
                stopButton.interactable = true;
            }
        }
    }

    private void Reset()
    {
        Time.timeScale = 0f;
        day = 1;
        min = 3;
        second = 60f;
        money = 0;
        timerText.text = string.Format($"{0:D2}:{1:D2}", min, (int)second);
        dayText.text = $"Day : {day}";
        moneyText.text = $"Money: ${money}";

    }

}
