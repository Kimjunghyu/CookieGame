using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject[] itemSlots;
    public Button[] buyButtons;

    private List<ShopData> bGradeItems;
    private List<ShopData> aGradeItems;

    public GameObject title;
    public TextMeshProUGUI shopGold;

    private void OnEnable()
    {
        if (ShopDataLoad.instance != null)
        {
            bGradeItems = ShopDataLoad.instance.GetBGradeItems();
            aGradeItems = ShopDataLoad.instance.GetAGradeItems();
            SetBGradeItems();
        }
        else
        {
            Debug.LogError("데이터 테이블 인스턴스x.");
        }

        shopGold.text = "0";
        if (GameManager.instance != null)
        {
            shopGold.text = GameManager.instance.totalMoney.ToString();
        }
    }


    private void SetBGradeItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < bGradeItems.Count)
            {
                DisplayItem(itemSlots[i], bGradeItems[i]);
                AddBuyButtonListener(buyButtons[i], bGradeItems[i]);
            }
            else
            {
                itemSlots[i].SetActive(false);
                buyButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetAGradeItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < aGradeItems.Count)
            {
                itemSlots[i].SetActive(true);
                buyButtons[i].gameObject.SetActive(true);
                DisplayItem(itemSlots[i], aGradeItems[i]);
                AddBuyButtonListener(buyButtons[i], aGradeItems[i]);
            }
            else
            {
                itemSlots[i].SetActive(false);
                buyButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void DisplayItem(GameObject slot, ShopData item)
    {
        slot.transform.Find("itemname").GetComponent<TextMeshProUGUI>().text = item.ProductName;
        slot.transform.GetComponentInChildren<Image>().sprite = LoadSprite(item.SpriteId);
        slot.transform.Find("iteminfo").GetComponent<TextMeshProUGUI>().text = item.ItemInfo;
        var button = slot.transform.GetComponentInChildren<Button>();
        button.transform.GetComponentInChildren<TextMeshProUGUI>().text = int.Parse(item.ProductPrice).ToString();
    }

    private void AddBuyButtonListener(Button button, ShopData item)
    {
        button.onClick.AddListener(() => BuyItem(item));
    }

    private void BuyItem(ShopData item)
    {
        if (bGradeItems.Contains(item))
        {
            bGradeItems.Remove(item);
        }
        else if (aGradeItems.Contains(item))
        {
            aGradeItems.Remove(item);
        }

        // 구매 처리 로직 추가
        // ...

        if (bGradeItems.Count <= 0)
        {
            SetAGradeItems();
        }
    }

    private Sprite LoadSprite(string spriteId)
    {
        return Resources.Load<Sprite>(spriteId);
    }

    public void OnClickExit()
    {
        if(!title.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            title.gameObject.SetActive(true);

        }
    }

    public void OnClickTopping()
    {

    }

    public void OnClickDough()
    {

    }

    public void OnClickBuy()
    {

    }
}
