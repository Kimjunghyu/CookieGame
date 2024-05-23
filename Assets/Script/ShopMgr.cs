using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopMgr : MonoBehaviour
{
    public GameObject title;
    public GameObject topping;
    public GameObject item;

    private void OnEnable()
    {
        if (!topping.gameObject.activeSelf)
        {
            topping.gameObject.SetActive(true);
        }
        if (item.gameObject.activeSelf)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void OnClickExit()
    {
        if (!title.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            title.gameObject.SetActive(true);
        }
    }

    public void OnClickTopping()
    {
        if (!topping.gameObject.activeSelf)
        {
            topping.gameObject.SetActive(true);
        }
        if (item.gameObject.activeSelf)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void OnClickItem()
    {
        if (!item.gameObject.activeSelf)
        {
            item.gameObject.SetActive(true);
        }
        if (topping.gameObject.activeSelf)
        {
            topping.gameObject.SetActive(false);
        }
    }
}
