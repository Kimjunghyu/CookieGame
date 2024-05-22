using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    private void Start()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        customer = GetComponentsInChildren<Image>();

        speed = Random.Range(4, 8);
        customerImage.sprite = customerImages[Random.Range(0, customerImages.Length)];
        slider = GetComponentInChildren<Slider>();
        slider.gameObject.SetActive(true);
        slider.value = slider.maxValue;
        slider.fillRect.GetComponentInChildren<Image>().sprite = maxValueImage;
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

    private void Update()
    {
        slider.value -= speed * Time.deltaTime;

        if(slider.value <= 19 )
        {
            SetSliderSprite();
        }

        if(slider.value <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
    private void SetSliderSprite()
    {
        var image = slider.fillRect.GetComponentInChildren<Image>();
        image.sprite = lowTimerSprite;
        image.type = Image.Type.Filled;
    }

    public void OnClickCustomer()
    {
        selectCookie = cookieImage;
        if(selectCookie != null && CookieManager.Instance.SelectedCookieImage != null)
        {
            if(selectCookie.sprite.name == CookieManager.Instance.SelectedCookieImage.sprite.name && CookieManager.Instance.resultCookieSelect)
            {
                foreach (var go in customer)
                {
                    go.gameObject.SetActive(false);
                }
                addCoin = 100;
                repute = 1;
                GameManager.instance.SetRepute(repute);
                GameManager.instance.AddMoney(addCoin);
                StartCoroutine(PrintCoin(addCoin, repute));
                CookieManager.Instance.OnClickTrash();
            }
            else if(CookieManager.Instance.SelectedCookieImage.sprite.name == CookieManager.Instance.burntCookie.name && CookieManager.Instance.resultCookieSelect)
            {
                foreach (var go in customer)
                {
                    go.gameObject.SetActive(false);
                }
                addCoin = 10;
                repute = -1;
                GameManager.instance.SetRepute(repute);
                GameManager.instance.AddMoney(addCoin);
                StartCoroutine(PrintCoin(addCoin, repute));
                CookieManager.Instance.OnClickTrash();
            }
            else
            {
                foreach (var go in customer)
                {
                    go.gameObject.SetActive(false);
                }
                addCoin = 50;
                repute = -1;
                GameManager.instance.SetRepute(repute);
                GameManager.instance.AddMoney(addCoin);
                StartCoroutine(PrintCoin(addCoin,repute));
                CookieManager.Instance.OnClickTrash();
            }
        }
        return;
    }

    private IEnumerator PrintCoin(int coinvalue, int reputevalue)
    {
        coin.gameObject.SetActive(true);
        if(reputevalue > 0)
        {
            coin.text = $"+{coinvalue}\n+{reputevalue}";
        }
        else
        {
            coin.text = $"+{coinvalue}\n{reputevalue}";
        }
        yield return new WaitForSeconds(1);
        coin.gameObject.SetActive(false);
        foreach (var go in customer)
        {
            go.gameObject.SetActive(true);
        }
        slider.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
