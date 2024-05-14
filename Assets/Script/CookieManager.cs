using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookieManager : MonoBehaviour
{
    public enum SelectionMode
    {
        Auto,
        Select
    }

    private SelectionMode selectionMode = SelectionMode.Auto;

    public static readonly string cookieTag = "Cookie";
    public static CookieManager Instance { get; private set; }
    public Image SelectedCookieImage { get; private set; }

    public Image[] images;
    public Image[] ovenImage;
    public Sprite prefabDoughC;
    public Sprite prefabCookieA;
    public Sprite prefabCookieB;

    private bool resultCookieSelect = false;
    private Image tableimage;
    private Image tableButtonImage;
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

    public void Start()
    {
        bool autoSelect = PlayerPrefs.GetInt("AutoToggle",1) == 1;
        if (autoSelect)
        {
            selectionMode = SelectionMode.Auto;
            Debug.Log("Auto mode is selected");
        }
        else
        {
            selectionMode = SelectionMode.Select;
            Debug.Log("Manual mode is selected");
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

    public void SetSelectionMode(SelectionMode mode)
    {
        selectionMode = mode;
    }

    public void OnClickDoughC()
    {
        if(selectionMode == SelectionMode.Auto)
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
       else
        {
            if(tableimage != null)
            {
                tableimage.gameObject.SetActive(true);
                tableimage.sprite = prefabDoughC;
                tableimage.tag = "Untagged";
            }
        }
    }

    public void OnClickToppingCa()
    {
        if(selectionMode == SelectionMode.Auto)
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
        else
        {
            if (tableimage != null)
            {
                if(tableimage.gameObject.activeSelf && tableimage.tag != cookieTag)
                {
                    tableimage.sprite = prefabCookieA;
                    tableimage.tag = cookieTag;
                } 
              
            }
        }
    }

    public void OnClickToppingCb()
    {
        if(selectionMode == SelectionMode.Auto)
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
        else
        {
            if (tableimage != null)
            {
                if (tableimage.gameObject.activeSelf && tableimage.tag != cookieTag)
                {
                    tableimage.sprite = prefabCookieB;
                    tableimage.tag = cookieTag;
                }

            }
        }
    }

    public void OnClickTrash()
    {
        if (tableimage != null)
        {
            tableimage.sprite = null;
            tableimage.gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void OnClickTableA()
    {
        if (tableButtonImage != null)
        {
            tableButtonImage.color = Color.white;
        }
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var buttonImage = button.GetComponent<Image>();
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
                resultCookieSelect = false;
            }
        }
    }

    public void OnClickOven()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
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
                    Sprite temp = tableimage.sprite;
                    if (foundImage != null && resultCookie != null)
                    {
                        StartCoroutine(StartOven(foundImage, resultCookie, temp));
                    }
                    tableimage.sprite = null;
                    tableimage.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator StartOven(Image foundImage, Image resultCookie, Sprite temp)
    {
        foundImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        foundImage.gameObject.SetActive(false);
        resultCookie.gameObject.SetActive(true);
        resultCookie.sprite = temp;
        resultCookie.type = Image.Type.Sliced;
    }

    public void EmptyTableImage()
    {
        tableimage.sprite = null;
        tableimage.tag = "Untagged";
        tableimage.gameObject.SetActive(false);
    }
}
