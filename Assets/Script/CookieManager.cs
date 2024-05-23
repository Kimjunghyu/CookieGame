using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookieManager : MonoBehaviour
{
    public static readonly string cookieTag = "Cookie";
    public static CookieManager Instance { get; private set; }
    public Image SelectedCookieImage { get; set; }
    public bool resultCookieSelect { get; set; }

    public Slider[] timers;
    public Slider[] burntTimer;

    public Image[] images;
    public Image[] ovenImage;
    public Sprite prefabDoughC;
    public Sprite prefabDoughB;
    public Sprite prefabDoughA;
    public Sprite prefabCookieA;
    public Sprite prefabCookieB;
    public Sprite burntCookie;

    private Image tableimage;
    private Image tableButtonImage;
    private Image buttonImage;

    private Image tempImage;

    private int value = 0;
    private bool isPlaying = false;

    public Button[] ovens;
    private Outline currentActiveOutline;

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
        foreach (var item in images)
        {
            item.sprite = null;
            item.tag = "Untagged";
            item.gameObject.SetActive(false);
        }
        foreach (var item in ovenImage)
        {
            item.sprite = null;
            item.gameObject.SetActive(false);
        }
        if (tableButtonImage != null)
        {
            tableButtonImage.color = Color.white;
        }
        foreach (var item in burntTimer)
            item.gameObject.SetActive(false);
        foreach (var item in timers)
            item.gameObject.SetActive(false);
    }

    private Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
    }

    private void Update()
    {
        isPlaying = GameManager.instance.isPlaying;

        if(SelectedCookieImage == null && currentActiveOutline != null)
        {
            currentActiveOutline.enabled = false;
        }
    }

    public void OnClickDoughC()
    {
        if (isPlaying)
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

    public void OnClickDoughB()
    {
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (!images[i].gameObject.activeSelf)
                {
                    images[i].gameObject.SetActive(true);
                    images[i].sprite = prefabDoughB;
                    images[i].tag = "Untagged";
                    break;
                }
            }
        }

        return;
    }

    public void OnClickDoughA()
    {
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (!images[i].gameObject.activeSelf)
                {
                    images[i].gameObject.SetActive(true);
                    images[i].sprite = prefabDoughA;
                    images[i].tag = "Untagged";
                    break;
                }
            }
        }

        return;
    }

    public void OnClickToppingCa()
    {
        value = 1;
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = LoadSprite(images[i].sprite.name + value);
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickToppingCb()
    {
        value = 2;
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = LoadSprite(images[i].sprite.name + value);
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickToppingBa()
    {
        value = 3;
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = LoadSprite(images[i].sprite.name + value);
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickToppingBb()
    {
        value = 4;
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = LoadSprite(images[i].sprite.name + value);
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickToppingAa()
    {
        value = 5;
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = LoadSprite(images[i].sprite.name + value);
                    images[i].tag = cookieTag;
                    break;
                }
            }
        }
        return;
    }

    public void OnClickToppingAb()
    {
        value = 6;
        if (isPlaying)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.activeSelf && images[i].tag != cookieTag)
                {
                    images[i].sprite = LoadSprite(images[i].sprite.name + value);
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
        if (isPlaying)
        {
            resultCookieSelect = false;
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
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
    }

    public void OnClickOven()
    {
        if (isPlaying)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                if (button != null)
                {
                    Image foundImage = null;
                    Image resultCookie = null;
                    Outline ovenOutline = null;

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

                        if (button.GetComponent<Outline>() != null)
                        {
                            ovenOutline = button.GetComponent<Outline>();
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
                    if (currentActiveOutline != null)
                    {
                        currentActiveOutline.enabled = false;
                    }
                    if (ovenOutline != null && resultCookie.gameObject.activeSelf)
                    {
                        ovenOutline.enabled = true;

                        currentActiveOutline = ovenOutline;
                    }
                }
            }
        }
    }

    private IEnumerator StartTimer(int index, Image ovenImage, Image resultImage)
    {
        ovenImage.gameObject.SetActive(true);
        burntTimer[index].gameObject.SetActive(true);
        burntTimer[index].value = 100;
        ovenImage.sprite = resultImage.sprite;
        tableimage.sprite = null;
        tableimage.tag = "Untagged";
        tableimage.gameObject.SetActive(false);
        burntTimer[index].gameObject.SetActive(false);
        timers[index].gameObject.SetActive(true);
        timers[index].value = 100;

        while (timers[index].value > 0)
        {
            timers[index].value -= (30 + GameManager.instance.ovenSpeed) * Time.deltaTime;
            yield return null;
        }

        timers[index].gameObject.SetActive(false);
        ovenImage.gameObject.SetActive(true);
        StartCoroutine(StartBurnt(index, ovenImage));
    }

    private IEnumerator StartBurnt(int index, Image ovenImage)
    {
        burntTimer[index].gameObject.SetActive(true);
        burntTimer[index].value = 100;

        while (burntTimer[index].value > 0)
        {
            burntTimer[index].value -= 30 * Time.deltaTime;
            yield return null;
        }
        ovenImage.sprite = burntCookie;
        burntTimer[index].gameObject.SetActive(false);
    }

    public void EmptyTableImage()
    {
        tableimage.sprite = null;
        tableimage.tag = "Untagged";
        tableimage.gameObject.SetActive(false);
    }
}
