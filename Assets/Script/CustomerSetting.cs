using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomerSetting : MonoBehaviour
{
    public Sprite[] customerImages;
    public Sprite[] cookies;
    public Image customerImage;
    public Sprite lowTimerSprite;

    public float speed;
    public Sprite maxValueImage;
    public Image cookieImage;
    private Slider slider;
    private Image selectCookie;
    private void Start()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        speed = Random.Range(4, 8);
        customerImage.sprite = customerImages[Random.Range(0, customerImages.Length)];
        slider = GetComponentInChildren<Slider>();
        slider.value = slider.maxValue;
        slider.fillRect.GetComponentInChildren<Image>().sprite = maxValueImage;
        if (cookies.Length > 0)
        {
            cookieImage.sprite = cookies[Random.Range(0, cookies.Length)];
        }
        else
        {
            Debug.LogError("Cookies array is empty.");
        }
    }

    private void Update()
    {
        slider.value -= speed * Time.deltaTime;

        if(slider.value <= 30 )
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
            if(selectCookie.sprite.name == CookieManager.Instance.SelectedCookieImage.sprite.name)
            {
                gameObject.SetActive(false);
                CookieManager.Instance.OnClickTrash();
                GameManager.instance.AddMoney(100);
            }
            else
            {
                return;
            }
        }
    }
}
