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
    public Image startImage;
    public Sprite ready;
    public Sprite go;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;

    private int day = 1;
    private int min = 3;
    private float second = 0f;
    public int money = 0;

    public bool gameOver = false;
    public bool isPlaying { get; private set; }
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
        timerText.text = $"{min:D2}:{(int)second:D2}";
        dayText.text = $"Day : {day}";
        moneyText.text = $"Money: ${money}";
    }
    private void Update()
    {

        if(isPlaying)
        {
            timerText.text = $"{min:D2}:{(int)second:D2}";
            second -= Time.deltaTime;
            if (second <= 0 && min < 0)
            {
                min -= 1;
                second = 60;
                
            }
            if (min <= 0 && second <= 0)
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
            isPlaying = false;
            OnClickQuit();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void OnClickStop()
    {
        if(!pause.activeSelf && isPlaying)
        {
            Time.timeScale = 0f;
            pause.SetActive(true);
            stopButton.interactable = false;
            isPlaying = false;
        }
    }

    public void OnClickResume()
    {
        if(pause.activeSelf)
        {
            Time.timeScale = 1f;
            pause.SetActive(false);
            stopButton.interactable = true;
            isPlaying = true;
        }
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
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
        timerText.text = $"{min:D2}:{(int)second:D2}";
        if (title.activeSelf)
        {
            gameOver = false;
            title.SetActive(false);
            stopButton.interactable = true;
        }
        StartCoroutine(StartMessage());
        if (!inGame.activeSelf)
        {
            inGame.SetActive(true);
            Time.timeScale = 1f;
           
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

    private IEnumerator StartMessage()
    {
        isPlaying = false;
        startImage.gameObject.SetActive(true);
        startImage.sprite = ready;
        yield return new WaitForSeconds(1);
        startImage.sprite = go;
        yield return new WaitForSeconds(1);
        startImage.gameObject.SetActive(false);
        isPlaying = true;
    }
}
