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

    private Image[] customer;
    public TextMeshProUGUI coin;

    public float speed;
    public Sprite maxValueImage;
    public Image cookieImage;
    private Slider slider;
    private Image selectCookie;

    private int addCoin;
    private int repute;

    private Coroutine printCoinCoroutine;

    private void Start()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        ApplyReputeData();
        customer = GetComponentsInChildren<Image>();

        customerImage.sprite = customerImages[Random.Range(0, customerImages.Length)];
        slider = GetComponentInChildren<Slider>();
        if (slider != null)
        {
            slider.gameObject.SetActive(true);
            slider.value = slider.maxValue;
            slider.fillRect.GetComponentInChildren<Image>().sprite = maxValueImage;
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
    }

    private void ApplyReputeData()
    {
        int currentRepute = GameManager.instance.repute;
        ReputeData reputeData = ReputeDataLoad.instance.GetReputeData(currentRepute);

        if (reputeData != null)
        {
            speed = Random.Range(reputeData.CusVisitTimerStart, reputeData.CusVisitTimerEnd);
        }
    }

    private void Update()
    {
        if (slider != null)
        {
            slider.value -= speed * Time.deltaTime;

            if (slider.value <= 19)
            {
                SetSliderSprite();
            }

            if (slider.value <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void SetSliderSprite()
    {
        if (slider != null)
        {
            var image = slider.fillRect.GetComponentInChildren<Image>();
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
                HandleCustomerInteraction(100, 1);
            }
            else if (CookieManager.Instance.SelectedCookieImage.sprite.name == CookieManager.Instance.burntCookie.name && CookieManager.Instance.resultCookieSelect)
            {
                HandleCustomerInteraction(10, -1);
            }
            else
            {
                HandleCustomerInteraction(50, -1);
            }
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

        if (slider != null)
        {
            slider.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
