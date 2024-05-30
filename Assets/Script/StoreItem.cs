using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public GameObject[] itemSlots;
    public Button[] buyButtons;
    public GameObject purchasePopup;
    public TextMeshProUGUI popupItemName;
    public Image popupImage;
    public TextMeshProUGUI popupItemPrice;
    public Button confirmButton;
    public Button cancelButton;

    private ShopItemData currentItem;
    private Button currentButton;

    private List<ShopItemData> items;
    public TextMeshProUGUI shopGold;

    private List<string> purchasedItem;
    public GameObject item;
    private void OnEnable()
    {
        purchasedItem = LoadPurchasedItems();

        if (ShopItemDataLoad.instance != null)
        {
            items = ShopItemDataLoad.instance.GetItems();
            SetItems();
        }

        if (GameManager.instance != null)
        {
            shopGold.text = GameManager.instance.totalMoney.ToString();
        }

        purchasePopup.SetActive(false);
    }

    private void SetItems()
    {
        int itemCount = items.Count;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < itemCount)
            {
                var item = items[i];
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

    private void SetButtonState(Button button, ShopItemData item)
    {
        if (!button.gameObject.activeSelf)
        {
            button.gameObject.SetActive(true);
        }

        if (purchasedItem.Contains(item.ProductName))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
            AddBuyButtonListener(button, item);
        }
    }

    private void DisplayItem(GameObject slot, ShopItemData item)
    {
        Transform itemNameTransform = slot.transform.Find("itemInfo/itemName/itemname");
        TextMeshProUGUI itemNameText = itemNameTransform.GetComponent<TextMeshProUGUI>();
        itemNameText.text = item.ProductName;

        Transform itemImageTransform = slot.transform.Find("itemInfo/itemImage");
        Image itemImage = itemImageTransform.GetComponent<Image>();
        itemImage.sprite = LoadSprite(item.ProductImage);

        Transform itemInfoTransform = slot.transform.Find("itemInfo/iteminfo");
        TextMeshProUGUI itemInfoText = itemInfoTransform.GetComponent<TextMeshProUGUI>();
        itemInfoText.text = item.ProductInfo;

        Transform buttonTransform = slot.transform.Find("buyButton");
        Button button = buttonTransform.GetComponent<Button>();
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>(true);
        buttonText.text = item.ProductPrice.ToString();
    }

    private void AddBuyButtonListener(Button button, ShopItemData item)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ShowPopup(button, item));
    }

    private void ShowPopup(Button button, ShopItemData item)
    {
        currentItem = item;
        currentButton = button;
        popupItemName.text = item.ProductName;
        popupImage.sprite = LoadSprite(item.ProductImage);
        popupItemPrice.text = item.ProductPrice.ToString();
        purchasePopup.SetActive(true);
    }

    public void OnClickBuy()
    {
        if (item.gameObject.activeSelf)
        {
            var value = currentItem.ProductPrice;
            if (GameManager.instance.totalMoney >= value)
            {
                GameManager.instance.totalMoney -= value;
                shopGold.text = GameManager.instance.totalMoney.ToString();
                purchasedItem.Add(currentItem.ProductName);
                SavePurchasedItems();
                GameManager.instance.SaveGameData();
                currentButton.interactable = false;
                SetItems();
                ApplyItemEffect(currentItem);
            }
            else
            {
                Debug.Log("Not enough money to buy item: " + currentItem.ProductName);
            }

            purchasePopup.SetActive(false);
        }

    }

    public static void ApplyPurchasedItemsEffect()
    {
        var purchasedItem = LoadPurchasedItems();
        var items = ShopItemDataLoad.instance?.GetItems();

        if (items == null) return;

        foreach (var itemName in purchasedItem)
        {
            var item = items.Find(i => i.ProductName == itemName);
            if (item != null)
            {
                ApplyItemEffect(item);
            }
        }
    }

    private static List<string> LoadPurchasedItems()
    {
        var savedItems = PlayerPrefs.GetString("PurchasedItem", "");
        if (!string.IsNullOrEmpty(savedItems))
        {
            return new List<string>(savedItems.Split(','));
        }
        return new List<string>();
    }

    private static void ApplyItemEffect(ShopItemData item)
    {
        switch (item.ProductEffect)
        {
            case 0:
                GameManager.instance.customerSpeed += item.EffectValue;
                break;
            case 1:
                GameManager.instance.addGold += item.EffectValue;
                break;
            case 2:
                GameManager.instance.ovenSpeed += item.EffectValue;
                break;
            default:
                break;
        }
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
        PlayerPrefs.SetString("PurchasedItem", string.Join(",", purchasedItem));
        PlayerPrefs.Save();
    }
}
