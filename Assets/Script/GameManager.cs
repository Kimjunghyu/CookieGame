    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    public class GameManager : MonoBehaviour
    {
    private readonly string cookieTag = "Cookie";

    public Image[] images;
    public Sprite prefabDoughC;
    public Sprite prefabCookieA;
    public Sprite prefabCookieB;
    public Image prefabCookieImage;


    private bool resultCookieSelect = false;
    private Image tableimage;
    private Image tableButtonImage;
    private Image result;
    public void OnClickDoughC()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (!images[i].gameObject.activeSelf)
            {
                images[i].gameObject.SetActive(true);
                images[i].sprite = prefabDoughC;

                break;
            }
        }
    }

    public void OnClickToppingCa()
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

    public void OnClickToppingCb()
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

    public void OnClickTrash()
    {
        if(tableimage != null)
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
        if(tableButtonImage != null)
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
                tableimage = resultCookie;
                resultCookieSelect = true;
                return;
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
                    else
                    {
                        return;
                    }
                    tableimage.sprite = null;
                    tableimage.tag = "Untagged";
                    tableimage.gameObject.SetActive(false);
                }
            }
            return;
        }
    }

    IEnumerator StartOven(Image foundImage, Image resultCookie, Sprite temp)
    {
        foundImage.gameObject.SetActive (true);
        yield return new WaitForSeconds(2);
        foundImage.gameObject.SetActive(false);
        resultCookie.gameObject.SetActive(true);
        resultCookie.sprite = temp;
        resultCookie.type = Image.Type.Sliced;
    }
    
}
