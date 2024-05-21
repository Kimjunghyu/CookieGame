using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject[] itemSlots;
    public Button[] buyButtons;

    public GameObject purchasePopup;
    public TextMeshProUGUI popupItemName;
    public TextMeshProUGUI popupItemPrice;
    public Button confirmButton;
    public Button cancelButton;

    private ShopData currentItem; // 현재 선택된 아이템
    private Button currentButton; // 현재 선택된 버튼

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

        purchasePopup.SetActive(false);
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
        Transform itemNameTransform = slot.transform.Find("itemInfo/itemName/itemname");
        TextMeshProUGUI itemNameText = itemNameTransform.GetComponent<TextMeshProUGUI>();
        itemNameText.text = item.ProductName;

        Transform itemImageTransform = slot.transform.Find("itemInfo/Image/itemImage");
        Image itemImage = itemImageTransform.GetComponent<Image>();
        itemImage.sprite = LoadSprite(item.SpriteId);

        Transform itemInfoTransform = slot.transform.Find("itemInfo/itemInfo/iteminfo");
        TextMeshProUGUI itemInfoText = itemInfoTransform.GetComponent<TextMeshProUGUI>();
        itemInfoText.text = item.ItemInfo;

        Transform buttonTransform = slot.transform.Find("buyButton");
        Button button = buttonTransform.GetComponent<Button>();
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>(true);
        buttonText.text = item.ProductPrice.ToString();
    }

    private void AddBuyButtonListener(Button button, ShopData item)
    {
        button.onClick.AddListener(() => ShowPopup(button, item));
    }

    private void ShowPopup(Button button, ShopData item)
    {
        currentItem = item;
        currentButton = button;
        popupItemName.text = item.ProductName;
        popupItemPrice.text = item.ProductPrice.ToString();
        purchasePopup.SetActive(true);
    }

    public void OnClickBuy()
    {
        //구매 했을시 행동 추가
        var value = currentItem.ProductPrice;
        if (GameManager.instance.totalMoney >= value)
        {
            GameManager.instance.totalMoney -= value;
            shopGold.text = GameManager.instance.totalMoney.ToString();

            if (bGradeItems.Contains(currentItem))
            {
                bGradeItems.Remove(currentItem);
            }
            else if (aGradeItems.Contains(currentItem))
            {
                aGradeItems.Remove(currentItem);
            }

            currentButton.transform.parent.gameObject.SetActive(false);
            currentButton.gameObject.SetActive(false);

            if (bGradeItems.Count <= 0)
            {
                SetAGradeItems();
            }
        }
        else
        {
            Debug.Log("Not enough money");
        }

        purchasePopup.SetActive(false);
    }

    public void ExitPopUp()
    {
        purchasePopup.SetActive(false);
    }

    private Sprite LoadSprite(string spriteId)
    {
        return Resources.Load<Sprite>(spriteId);
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

    }

    public void OnClickDough()
    {

    }
}
