using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private float second = 0f;
    public int money = 0;
    private int stage = 0;
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
        SetStageTimer(day);
        UpdateUI();
    }

    private void Update()
    {
        if (isPlaying)
        {
            timerText.text = $"{(int)second / 60:D2}:{(int)second % 60:D2}";
            second -= Time.deltaTime;
            if (second <= 0)
            {
                day++;
                SetStageTimer(day);
                UpdateUI();
            }
        }
    }

    public void OnClickStop()
    {
        if (!pause.activeSelf && isPlaying)
        {
            Time.timeScale = 0f;
            pause.SetActive(true);
            stopButton.interactable = false;
            isPlaying = false;
        }
    }

    public void OnClickResume()
    {
        if (pause.activeSelf)
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
        if (!title.activeSelf)
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
        moneyText.text = $"${money}";
    }

    public void OnClickStart()
    {
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

    private void SetStageTimer(int day)
    {
        if(day <= 2)
        {
            stage = 1;
        }
        else if(day <=5)
        {
            stage = 2;
        }
        else if(day <= 9)
        {
            stage = 3;
        }
        else if(day <= 14)
        {
            stage = 4;
        }
        else if(day <= 20)
        {
            stage = 5;
        }
        else if(day <= 24)
        {
            stage = 6;
        }
        else
        {
            stage = 7;
        }
     
        StageData stageData = StageDataLoad.instance.GetStageData(stage - 1);
        if (stageData != null)
        {
            second = stageData.stageTimer;
        }
        else
        {
            Debug.LogError("스테이지 데이터 불러오기x.");
            second = 180;
        }
    }

    private void UpdateUI()
    {
        timerText.text = $"{(int)second / 60:D2}:{(int)second % 60:D2}";
        dayText.text = $"Day : {day}";
        moneyText.text = $"${money}";
    }

    private void Reset()
    {
        Time.timeScale = 0f;
        day = 1;
        SetStageTimer(day);
        money = 0;
        UpdateUI();
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
