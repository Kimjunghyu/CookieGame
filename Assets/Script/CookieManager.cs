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

    public AudioClip doughSound;
    public AudioClip toppingSound;
    public AudioClip ovenSound;
    private AudioSource audioSource;
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

    private bool[] isBaking;
    private Coroutine[] bakingCoroutines;
    private Coroutine[] burningCoroutines;

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
        audioSource = GetComponent<AudioSource>();
        isBaking = new bool[ovenImage.Length];
        bakingCoroutines = new Coroutine[ovenImage.Length];
        burningCoroutines = new Coroutine[ovenImage.Length];
    }

    private void OnEnable()
    {
        SelectedCookieImage = null;
        resultCookieSelect = false;
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
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in timers)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < isBaking.Length; i++)
        {
            isBaking[i] = false;
            if (bakingCoroutines[i] != null)
            {
                StopCoroutine(bakingCoroutines[i]);
            }
            if (burningCoroutines[i] != null)
            {
                StopCoroutine(burningCoroutines[i]);
            }
        }
    }

    private void Update()
    {
        isPlaying = GameManager.instance.isPlaying;

        if (SelectedCookieImage == null && currentActiveOutline != null)
        {
            currentActiveOutline.enabled = false;
        }
    }

    private Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(doughSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(doughSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(doughSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(toppingSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(toppingSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(toppingSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(toppingSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(toppingSound);
                    }
                    break;
                }
            }
        }
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
                    if (GameManager.instance.soundEffect)
                    {
                        audioSource.PlayOneShot(toppingSound);
                    }
                    break;
                }
            }
        }
    }

    public void OnClickTrash()
    {
        if (tableimage != null && isPlaying)
        {
            tableimage.sprite = null;
            tableimage.gameObject.SetActive(false);
            SelectedCookieImage = null;
            if (!resultCookieSelect)
            {
                tableimage.tag = "Untagged";
            }
        }
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

                                // 기존 코루틴이 실행 중이면 중지
                                if (bakingCoroutines[i] != null)
                                {
                                    StopCoroutine(bakingCoroutines[i]);
                                }
                                if (burningCoroutines[i] != null)
                                {
                                    StopCoroutine(burningCoroutines[i]);
                                }

                                // 코루틴 시작 및 상태 갱신
                                bakingCoroutines[i] = StartCoroutine(StartTimer(i, ovenImage[i], tempImage));
                                isBaking[i] = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // buttonImage.color = Color.white;
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
                        bool isAnyTimerActive = false;
                        foreach (Slider slider in button.GetComponentsInChildren<Slider>())
                        {
                            if (slider.CompareTag("CookieTimer") && slider.gameObject.activeSelf)
                            {
                                isAnyTimerActive = true;
                                break;
                            }
                        }

                        if (isAnyTimerActive)
                        {
                            return;
                        }

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
            while (!GameManager.instance.isPlaying)
            {
                yield return null;
            }

            timers[index].value -= (30 + GameManager.instance.ovenSpeed) * Time.deltaTime;
            yield return null;
        }

        timers[index].gameObject.SetActive(false);
        isBaking[index] = false;

        burningCoroutines[index] = StartCoroutine(StartBurnt(index, ovenImage));
    }

    private IEnumerator StartBurnt(int index, Image ovenImage)
    {
        if (GameManager.instance.soundEffect)
        {
            audioSource.PlayOneShot(ovenSound);
        }
        burntTimer[index].gameObject.SetActive(true);
        burntTimer[index].value = 100;

        while (burntTimer[index].value > 0)
        {
            while (!GameManager.instance.isPlaying)
            {
                yield return null;
            }

            burntTimer[index].value -= 30 * Time.deltaTime;
            yield return null;
        }

        ovenImage.sprite = burntCookie;
        burntTimer[index].gameObject.SetActive(false);
    }

    public void EmptyTableImage()
    {
        if (tableimage != null)
        {
            tableimage.sprite = null;
            tableimage.tag = "Untagged";
            tableimage.gameObject.SetActive(false);
        }
    }
}
