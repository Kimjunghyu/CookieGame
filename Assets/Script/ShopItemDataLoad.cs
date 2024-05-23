using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemDataLoad : MonoBehaviour
{
    public static ShopItemDataLoad instance;
    public string csvFilePath = "ShopItemTable";

    private List<ShopItemData> items = new List<ShopItemData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadItemData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadItemData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFilePath);

        if (csvFile != null)
        {
            Debug.Log("File loaded successfully");
            string[] lines = csvFile.text.Split('\n');
            int expectedColumnCount = 8;

            bool isFirstLine = true;
            foreach (string line in lines)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                if (string.IsNullOrEmpty(line)) continue;

                string[] row = line.Split(',');
                if (row.Length != expectedColumnCount)
                {
                    continue;
                }

                try
                {
                    int productPrice = int.Parse(row[3].Replace(",", "").Replace("\"", "").Trim());

                    ShopItemData newItem = new ShopItemData(
                        int.Parse(row[0]),
                        row[1],
                        int.Parse(row[2]),
                        productPrice,
                        row[4],
                        int.Parse(row[5]),
                        int.Parse(row[6]),
                        row[7]
                    );

                    items.Add(newItem);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing row: " + e.Message);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to load data from ShopData at path: " + csvFilePath);
        }
    }

    public List<ShopItemData> GetItems()
    {
        return items;
    }
}

[System.Serializable]
public class ShopItemData
{
    public int ProductID;
    public string ProductName;
    public int ProductType;
    public int ProductPrice;
    public string ProductImage;
    public int ProductEffect;
    public int EffectValue;
    public string ProductInfo;

    public ShopItemData(int productID, string productName, int productType, int productPrice, string productImage, int productEffect, int effectValue, string productInfo)
    {
        ProductID = productID;
        ProductName = productName;
        ProductType = productType;
        ProductPrice = productPrice;
        ProductImage = productImage;
        ProductEffect = productEffect;
        EffectValue = effectValue;
        ProductInfo = productInfo;
    }
}