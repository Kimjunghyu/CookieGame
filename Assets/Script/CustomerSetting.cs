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
            Debug.Log("cookieImage.sprite set to: " + cookieImage.sprite.name);
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
        Debug.Log("ƒÌ≈∞¿Ã∏ß :" + CookieManager.Instance.SelectedCookieImage.sprite.name);

        if(selectCookie != null && CookieManager.Instance.SelectedCookieImage != null)
        {
            if(selectCookie.sprite.name == CookieManager.Instance.SelectedCookieImage.sprite.name)
            {
                gameObject.SetActive(false);
                //CookieManager.Instance.EmptyTableImage();
                CookieManager.Instance.OnClickTrash();
            }
        }
        //if (CookieManager.Instance != null && CookieManager.Instance.SelectedCookieImage != null && cookieImage.sprite != null)
        //{
        //    if (selectCookie.sprite.name == CookieManager.Instance.SelectedCookieImage.name)
        //    {
        //        Debug.Log("cookiename :" + cookieImage.sprite.name);
        //        Debug.Log("managername: " + CookieManager.Instance.SelectedCookieImage.name);
        //        gameObject.SetActive(false);
        //        //CookieManager.Instance.EmptyTableImage();
        //    }
        //}
        //if (CookieManager.Instance == null)
        //{
        //    Debug.LogError("CookieManager.Instance == null");
        //}
        //if (CookieManager.Instance.SelectedCookieImage.sprite.name == null)
        //{
        //    Debug.LogError("CookieManager.Instance.SelectedCookieImage == null");
        //}
        //Debug.Log("SelectCookie : " + selectCookie.sprite.name);
        return;
    }
}
