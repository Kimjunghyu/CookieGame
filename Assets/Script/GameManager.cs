using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pause;
    public Button stopButton;
    public GameObject title;
    public GameObject inGame;
    public GameObject result;
    public GameObject shop;

    public Image startImage;
    public Sprite ready;
    public Sprite go;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;
    public AudioSource mainBGM;
    public AudioSource gameBGM;
    public int day { get; set; }
    public int totalMoney { get; set; }
    public int repute {  get; set; }

    private float second = 0f;
    public int resultTotalMoney { get; set; }
    public int money { get; set; }
    public int stage { get; private set; }
    public int tax { get; set; }
    public float bgmValue { get; set; }
    public bool gameOver = false;
    public bool isPlaying { get; set; }
    public bool bgmPlaying = true;
    public int currCoin = 0;
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
        tax = 0;
        bgmValue = 1f;
        if (title.activeSelf)
        {
            PlayMainBGM();
        }
        ResetPlayerPrefs();
        totalMoney = 99999;
        resultTotalMoney = totalMoney;
        day = 1;
        stage = 0;
        SetStageTimer(day);
        UpdateUI();
        UpdateTax();
    }

    private void Update()
    {
        if(bgmPlaying)
        {
            if (mainBGM != null)
            {
                mainBGM.volume = bgmValue;
            }
            if (gameBGM != null)
            {
                gameBGM.volume = bgmValue;
            }

            if (title.activeSelf && !mainBGM.isPlaying)
            {
                PlayMainBGM();
            }
            else if (inGame.activeSelf && !gameBGM.isPlaying)
            {
                PlayGameBGM();
            }
        }
        else
        {
            mainBGM.Stop();
            gameBGM.Stop();
        }

        if (isPlaying)
        {
            timerText.text = $"{(int)second / 60:D2}:{(int)second % 60:D2}";
            second -= Time.deltaTime;
            if (second <= 0)
            {
                day++;
                SetStageTimer(day);
                UpdateUI();
                if(!result.gameObject.activeSelf)
                {
                    isPlaying = false;
                    totalMoney += money;
                    currCoin = totalMoney - tax;
                    inGame.gameObject.SetActive(false);
                    result.gameObject.SetActive(true);
                }
            }
        }
    }

    private void PlayMainBGM()
    {
        if (gameBGM.isPlaying)
        {
            gameBGM.Stop();
        }
        mainBGM.Play();
    }

    private void PlayGameBGM()
    {
        if (mainBGM.isPlaying)
        {
            mainBGM.Stop();
        }
        gameBGM.Play();
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
        if (!title.activeSelf)
        {
            title.SetActive(true);
            pause.SetActive(false);
            inGame.SetActive(false);

            gameOver = true;
        }

    }

    public void OnClickGameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
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
        money = 0;
        SetStageTimer(day);
        UpdateUI();
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
            second = 180;
        }
    }

    public void SetRepute(int value)
    {
        repute += value;
        if (repute < 0)
        {
            repute = 0;
        }
        UpdateTax();
    }
    private void UpdateTax()
    {
        ReputeData reputeData = ReputeDataLoad.instance.GetReputeData(repute);
        if (reputeData != null)
        {
            tax = reputeData.Tax;
        }
        else
        {
            tax = 0;
        }
    }
    private void UpdateUI()
    {
        timerText.text = $"{(int)second / 60:D2}:{(int)second % 60:D2}";
        dayText.text = $"{day}" + $"{TransrationKr.instance.GetTranslation("Day")}";
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

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
