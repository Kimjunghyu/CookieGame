using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject[] itemSlots;
    public Button[] buyButtons;

    public Button[] itemArray;

    public GameObject purchasePopup;
    public TextMeshProUGUI popupItemName;
    public Image popupImage;
    public TextMeshProUGUI popupItemPrice;
    public Button confirmButton;
    public Button cancelButton;

    private ShopData currentItem;
    private Button currentButton;

    private List<ShopData> bGradeItems;
    private List<ShopData> aGradeItems;

    public GameObject title;
    public TextMeshProUGUI shopGold;

    private List<string> purchasedItems;
    private int CookiebuyCount = 0;

    public GameObject topping;
    public GameObject item;

    private void OnEnable()
    {
        purchasedItems = LoadPurchasedItems();

        if (ShopDataLoad.instance != null)
        {
            bGradeItems = ShopDataLoad.instance.GetBGradeItems();
            aGradeItems = ShopDataLoad.instance.GetAGradeItems();
            SetItems();
        }

        shopGold.text = "0";
        if (GameManager.instance != null)
        {
            shopGold.text = GameManager.instance.totalMoney.ToString();
        }

        purchasePopup.SetActive(false);

    }

    private void SetItems()
    {
        int bGradeCount = bGradeItems.Count;
        int aGradeCount = aGradeItems.Count;

        bool showAGradeItems = CookiebuyCount >= 3;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!showAGradeItems)
            {
                if (i < bGradeCount)
                {
                    var item = bGradeItems[i];
                    itemSlots[i].SetActive(true);
                    DisplayItem(itemSlots[i], item);
                    SetButtonState(buyButtons[i], item);
                }
                else
                {
                    itemSlots[i].SetActive(false);
                    buyButtons[i].gameObject.SetActive(false);
                }
            }
            else
            {
                if (i < aGradeCount)
                {
                    var item = aGradeItems[i];
                    itemSlots[i].SetActive(true);
                    DisplayItem(itemSlots[i], item);
                    SetButtonState(buyButtons[i], item);
                }
                else
                {
                    itemSlots[i].SetActive(false);
                    buyButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void SetButtonState(Button button, ShopData item)
    {
        if (!button.gameObject.activeSelf)
        {
            button.gameObject.SetActive(true);
        }

        if (purchasedItems.Contains(item.ProductName))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
            AddBuyButtonListener(button, item);
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
        popupImage.sprite = LoadSprite(item.SpriteId);
        popupItemPrice.text = item.ProductPrice.ToString();
        purchasePopup.SetActive(true);
    }

    public void OnClickBuy()
    {
        var value = currentItem.ProductPrice;
        if (GameManager.instance.totalMoney >= value)
        {
            GameManager.instance.totalMoney -= value;
            shopGold.text = GameManager.instance.totalMoney.ToString();

            purchasedItems.Add(currentItem.ProductName);
            if (topping.gameObject.activeSelf)
            {
                CookiebuyCount++;
            }
            SavePurchasedItems();
            currentButton.interactable = false;

            // 구매한 항목 버튼 활성화
            foreach (var item in itemArray)
            {
                if (item != null && item.name == currentItem.IngButtonImage)
                {
                    item.interactable = true;
                }
            }

            SetItems();
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

    private void SavePurchasedItems()
    {
        PlayerPrefs.SetString("PurchasedItems", string.Join(",", purchasedItems));
        PlayerPrefs.SetInt("CookiebuyCount", CookiebuyCount);
        PlayerPrefs.Save();
    }



    public static void ActivatePurchasedItems(Button[] itemArray)
    {
        var purchasedItems = LoadPurchasedItems();

        // 구매한 항목 버튼 활성화
        foreach (var item in itemArray)
        {
            if (item != null && purchasedItems.Contains(item.name))
            {
                item.interactable = true;
            }
        }
    }
    //private List<string> LoadPurchasedItems()
    //{
    //    var savedItems = PlayerPrefs.GetString("PurchasedItems", "");
    //    CookiebuyCount = PlayerPrefs.GetInt("CookiebuyCount", 0);
    //    if (!string.IsNullOrEmpty(savedItems))
    //    {
    //        return new List<string>(savedItems.Split(','));
    //    }
    //    return new List<string>();
    //}
    private static List<string> LoadPurchasedItems()
    {
        var savedItems = PlayerPrefs.GetString("PurchasedItems", "");
        if (!string.IsNullOrEmpty(savedItems))
        {
            return new List<string>(savedItems.Split(','));
        }
        return new List<string>();
    }
}
