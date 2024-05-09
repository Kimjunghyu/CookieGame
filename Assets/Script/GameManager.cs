using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Image[] images;
    public Sprite prefabDoughC;
    public Sprite prefabCookieA;
    public Sprite prefabCookieB;

    private Image tableimage;
    private Image tableButtonImage;

    //private List<Image> cookieDoughs = new List<Image>();
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
            if (images[i].gameObject.activeSelf && images[i].tag != "Cookie")
            {
                images[i].sprite = prefabCookieA;
                images[i].tag = "Cookie";
                break;
            }
        }
    }

    public void OnClickToppingCb()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].gameObject.activeSelf && images[i].tag != "Cookie")
            {
                images[i].sprite = prefabCookieB;
                images[i].tag = "Cookie";
                break;
            }
        }
    }

    public void OnClickTrash()
    {
        if(tableimage != null)
        {
            tableimage.sprite = null;
            tableimage.tag = "Untagged";
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
            }
        }
    }

    public void OnClickOven()
    {
        if(tableimage!=null)
        {
            if(tableimage.sprite != null && tableimage.tag == "Cookie")
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
                            if (image.tag == "Cookie")
                            {
                                resultCookie = image;
                            }
                            else
                            {
                                foundImage = image;
                            }
                            
                        }
                    }
                    if (foundImage != null && resultCookie != null)
                    {
                        StartCoroutine(StartOven(foundImage, resultCookie,tableimage));
                    }
                    //tableimage.sprite = null;
                    //tableimage.tag = "Untagged";
                    //tableimage.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator StartOven(Image foundImage, Image resultCookie, Image table)
    {
        foundImage.gameObject.SetActive (true);
        yield return new WaitForSeconds(2);
        foundImage.gameObject.SetActive(false);
        resultCookie.gameObject.SetActive(true);
        resultCookie.sprite = table.sprite;
        //tableimage.sprite = null;
        //tableimage.tag = "Untagged";
        //tableimage.gameObject.SetActive(false);
        StopCoroutine(StartOven(foundImage, resultCookie, table));
    }
    
}
