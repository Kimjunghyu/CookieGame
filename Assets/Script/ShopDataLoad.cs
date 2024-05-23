using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDataLoad : MonoBehaviour
{
    public static ShopDataLoad instance;
    public string csvFilePath = "ShopIngTable"; // 확장자 제외

    private List<ShopData> items = new List<ShopData>();
    private List<ShopData> bGradeItems = new List<ShopData>();
    private List<ShopData> aGradeItems = new List<ShopData>();

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
        Debug.Log("Attempting to load file from path: " + csvFilePath);

        if (csvFile != null)
        {
            Debug.Log("File loaded successfully");
            string[] lines = csvFile.text.Split('\n');
            int expectedColumnCount = 9;

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

                    ShopData newItem = new ShopData(
                        int.Parse(row[0]),
                        row[1],
                        int.Parse(row[2]),
                        productPrice,
                        row[4],
                        int.Parse(row[5]),
                        row[6],
                        row[7],
                        row[8]
                    );

                    items.Add(newItem);
                    if (newItem.ProductLevel == 0)
                    {
                        bGradeItems.Add(newItem);
                    }
                    else
                    {
                        aGradeItems.Add(newItem);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing row: " + e.Message);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to load data from ShopIngTable at path: " + csvFilePath);
        }
    }

    public List<ShopData> GetBGradeItems()
    {
        return bGradeItems;
    }

    public List<ShopData> GetAGradeItems()
    {
        return aGradeItems;
    }
}

[System.Serializable]
public class ShopData
{
    public int ProductID;
    public string ProductName;
    public int ProductType;
    public int ProductPrice;
    public string ProductImage;
    public int ProductLevel;
    public string IngButtonImage;
    public string SpriteId;
    public string ItemInfo;

    public ShopData(int productID, string productName, int productType, int productPrice, string productImage, int productLevel, string ingButtonImage, string spriteId, string itemInfo)
    {
        ProductID = productID;
        ProductName = productName;
        ProductType = productType;
        ProductPrice = productPrice;
        ProductImage = productImage;
        ProductLevel = productLevel;
        IngButtonImage = ingButtonImage;
        SpriteId = spriteId;
        ItemInfo = itemInfo;
    }
}
