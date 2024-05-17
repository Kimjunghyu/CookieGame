using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookieManager : MonoBehaviour
{
    public static readonly string cookieTag = "Cookie";
    public static CookieManager Instance { get; private set; }
    public Image SelectedCookieImage { get; set; }
    public bool resultCookieSelect { get; private set; }

    public Slider[] timers;
    public Image[] images;
    public Image[] ovenImage;
    public Sprite prefabDoughC;
    public Sprite prefabCookieA;
    public Sprite prefabCookieB;

    private Image tableimage;
    private Image tableButtonImage;
    private Image buttonImage;

    private Image tempImage;
    private bool isPlaying = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        foreach(var item in images)
        {
            item.sprite = null;
            item.tag = "Untagged";
            item.gameObject.SetActive(false);
        }
        foreach(var item in ovenImage)
        {
            item.sprite = null;
            item.gameObject.SetActive(false);
        }
        if(tableButtonImage != null)
        {
            tableButtonImage.color = Color.white;
        }
    }

    private void Update()
    {
        isPlaying = GameManager.instance.isPlaying;
    }

    public void OnClickDoughC()
    {
        if(isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (!images[i].gameObject.activeSelf)
                {
                    images[i].gameObject.SetActive(true);
                    images[i].sprite = prefabDoughC;
                    images[i].tag = "Untagged";
                    break;
                }
            }
        }

        return;
    }

    public void OnClickToppingCa()
    {
        if(isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = prefabCookieA;
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickToppingCb()
    {
        if(isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = prefabCookieB;
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickTrash()
    {
        if (tableimage != null && isPlaying)
        {
            tableimage.sprite = null;
            tableimage.gameObject.SetActive(false);
            if (!resultCookieSelect)
            {
                tableimage.tag = "Untagged";
            }
        }
        return;
    }

    public void OnClickTableA()
    {
        if(isPlaying)
        {
            resultCookieSelect = false;
            if (tableButtonImage != null)
            {
                tableButtonImage.color = Color.white;
            }

            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            buttonImage = button.GetComponent<Image>();
            buttonImage.color = Color.red;

            tableButtonImage = buttonImage;
            if (button != null)
            {
                Image foundImage = null;
                foreach (Transform child in button.transform)
                {
                    Image image = child.GetComponent<Image>();
                    if (image != null)
                    {
                        foundImage = image;
                        break;
                    }
                }
                if (foundImage != null)
                {

                    tableimage = foundImage;

                    for (int i = 0; i < ovenImage.Length; ++i)
                    {
                        if (!ovenImage[i].gameObject.activeSelf && foundImage.tag == cookieTag && !timers[i].gameObject.activeSelf)
                        {
                            tempImage = foundImage;
                            StartCoroutine(StartTimer(i, ovenImage[i], tempImage));
                            //ovenImage[i].gameObject.SetActive(true);
                            //ovenImage[i].sprite = foundImage.sprite;

                            break;
                        }
                    }

                }
                else
                {
                    buttonImage.color = Color.white;
                }
            }
        }
    }

    private IEnumerator StartTimer(int index, Image ovenImage, Image resultImage)
    {
        ovenImage.gameObject.SetActive(true);
        ovenImage.sprite = resultImage.sprite;
        ovenImage.gameObject.SetActive(false);
        var temp = resultImage;
        tableimage.sprite = null;
        tableimage.tag = "Untagged";
        tableimage.gameObject.SetActive(false);
        timers[index].gameObject.SetActive(true);
        timers[index].value = 100;

        while (timers[index].value > 0)
        {
            timers[index].value -= 30 * Time.deltaTime;
            yield return null;
        }

        timers[index].gameObject.SetActive(false);
        ovenImage.gameObject.SetActive(true);
        //ovenImage.sprite = temp.sprite;
    }
    public void OnClickOven()
    {
        if (isPlaying)
        {
            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if (button.image != buttonImage)
            {
                buttonImage.color = Color.white;
            }
            if (button != null)
            {
                Image foundImage = null;
                Image resultCookie = null;
                foreach (Transform child in button.transform)
                {
                    Image image = child.GetComponent<Image>();
                    if (image != null)
                    {
                        if (image.tag == cookieTag)
                        {
                            resultCookie = image;
                        }
                        else
                        {
                            foundImage = image;
                        }
                    }
                }

                if (resultCookie != null && resultCookie.gameObject.activeSelf)
                {
                    SelectedCookieImage = resultCookie;
                    tableimage = resultCookie;
                    resultCookieSelect = true;
                }
                else if (tableimage != null && !resultCookieSelect)
                {
                    if (tableimage.sprite != null && tableimage.tag == cookieTag)
                    {
                        tableimage.sprite = null;
                        tableimage.tag = "Untagged";
                        tableimage.gameObject.SetActive(false);
                    }
                }
            }
        }
      
    }

    public void EmptyTableImage()
    {
        tableimage.sprite = null;
        tableimage.tag = "Untagged";
        tableimage.gameObject.SetActive(false);
    }
}
