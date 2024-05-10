using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        cookieImage.sprite = cookies[Random.Range(0, cookies.Length)];
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
}
