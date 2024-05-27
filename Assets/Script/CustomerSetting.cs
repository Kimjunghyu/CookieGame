using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerSetting : MonoBehaviour
{
    public Sprite[] customerImages;
    public Sprite[] cookies;
    public Image customerImage;
    public Sprite lowTimerSprite;
    public Slider customerTimer;

    private Image[] customer;
    public TextMeshProUGUI coin;

    public float speed;
    public Sprite maxValueImage;
    public Image cookieImage;
    private Image selectCookie;

    private int addCoin;
    private int repute;

    private Coroutine printCoinCoroutine;
    private bool timeOver = false;

    private void Start()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable called for " + gameObject.name);

        ApplyReputeData();
        customer = GetComponentsInChildren<Image>();

        if (!customerImage.gameObject.activeSelf && customerImage != null)
        {
            customerImage.gameObject.SetActive(true);
        }
        if (coin.gameObject.activeSelf && coin != null)
        {
            coin.gameObject.SetActive(false);
        }

        customerImage.sprite = customerImages[Random.Range(0, customerImages.Length)];

        if (customerTimer != null)
        {
            customerTimer.gameObject.SetActive(true);
            customerTimer.value = customerTimer.maxValue;
            customerTimer.fillRect.GetComponentInChildren<Image>().sprite = maxValueImage;
        }

        int currentStage = GameManager.instance.stage;
        StageData stageData = StageDataLoad.instance.GetStageData(currentStage - 1);
        if (stageData != null)
        {
            int cookieStart = stageData.cookieStart;
            int cookieEnd = stageData.cookieEnd;
            if (cookies.Length > 0 && cookieStart >= 0 && cookieEnd < cookies.Length)
            {
                cookieImage.sprite = cookies[Random.Range(cookieStart, cookieEnd + 1)];
            }
        }
        timeOver = false;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable called for " + gameObject.name);

        if (customerTimer != null)
        {
            customerTimer.gameObject.SetActive(false);
        }
        if (coin != null)
        {
            coin.gameObject.SetActive(false);
        }
        if (printCoinCoroutine != null)
        {
            StopCoroutine(printCoinCoroutine);
        }
    }

    private void ApplyReputeData()
    {
        int currentRepute = GameManager.instance.repute;
        ReputeData reputeData = ReputeDataLoad.instance.GetReputeData(currentRepute);

        if (reputeData != null)
        {
            speed = Random.Range(5 + GameManager.instance.customerSpeed, 7 + GameManager.instance.customerSpeed);
        }
    }

    private void Update()
    {
        if (customerTimer != null && !timeOver)
        {
            customerTimer .value -= speed * Time.deltaTime;

            if (customerTimer.value <= 19)
            {
                SetSliderSprite();
            }

            if (customerTimer.value <= 0)
            {
                timeOver = true;
                HandleCustomerInteraction(0, -1);
            }
        }
    }

    private void SetSliderSprite()
    {
        if (customerTimer != null)
        {
            var image = customerTimer.fillRect.GetComponentInChildren<Image>();
            if (image != null)
            {
                image.sprite = lowTimerSprite;
                image.type = Image.Type.Filled;
            }
        }
    }

    public void OnClickCustomer()
    {
        selectCookie = cookieImage;
        if (selectCookie != null && CookieManager.Instance.SelectedCookieImage != null)
        {
            if (selectCookie.sprite.name == CookieManager.Instance.SelectedCookieImage.sprite.name && CookieManager.Instance.resultCookieSelect)
            {
                HandleCustomerInteraction(100 + GameManager.instance.addGold, 1);
            }
            else if (CookieManager.Instance.SelectedCookieImage.sprite.name == CookieManager.Instance.burntCookie.name && CookieManager.Instance.resultCookieSelect)
            {
                HandleCustomerInteraction(10 + GameManager.instance.addGold, -1);
            }
            else if (selectCookie.sprite.name != CookieManager.Instance.SelectedCookieImage.sprite.name && CookieManager.Instance.resultCookieSelect)
            {
                HandleCustomerInteraction(50 + GameManager.instance.addGold, -1);
            }
            else
            {
                return;
            }
        }
        else if(CookieManager.Instance.SelectedCookieImage == null)
        {
            return;
        }
    }

    private void HandleCustomerInteraction(int coinAmount, int reputeChange)
    {
        CookieManager.Instance.SelectedCookieImage = null;
        foreach (var go in customer)
        {
            go.gameObject.SetActive(false);
        }

        addCoin = coinAmount;
        repute = reputeChange;
        GameManager.instance.SetRepute(repute);
        GameManager.instance.AddMoney(addCoin);

        if (printCoinCoroutine != null)
        {
            StopCoroutine(printCoinCoroutine);
        }
        printCoinCoroutine = StartCoroutine(PrintCoin(addCoin, repute));
        CookieManager.Instance.OnClickTrash();
    }

    private IEnumerator PrintCoin(int coinvalue, int reputevalue)
    {
        if (coin != null)
        {
            coin.gameObject.SetActive(true);
            coin.text = reputevalue > 0 ? $"+{coinvalue}$\n{TransrationKr.instance.GetTranslation("Repute")}+{reputevalue}" : $"+{coinvalue}$\n{TransrationKr.instance.GetTranslation("Repute")}{reputevalue}";
            yield return new WaitForSeconds(1);
            coin.gameObject.SetActive(false);
        }

        foreach (var go in customer)
        {
            if (go != null)
            {
                go.gameObject.SetActive(true);
            }
        }

        if (customerTimer != null)
        {
            customerTimer.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
