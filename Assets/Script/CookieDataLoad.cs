using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieDataLoad : MonoBehaviour
{
    public static CookieDataLoad instance;
    public string csvFilePath = "CookieTable";

    private List<CookieData> cookieItems = new List<CookieData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCookieData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCookieData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFilePath);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');
            int expectedColumnCount = 5;

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
                    CookieData newCookie = new CookieData(
                        int.Parse(row[0]),
                        int.Parse(row[1]),
                        row[2],
                        row[3],
                        row[4]
                    );

                    cookieItems.Add(newCookie);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Load False");
                }
            }
        }
    }

    public List<CookieData> GetCookieItems()
    {
        return cookieItems;
    }
}

[System.Serializable]
public class CookieData
{
    public int CookieID;
    public int CookiePrice;
    public string Ing1ID;
    public string Ing2ID;
    public string CookieImage;

    public CookieData(int cookieID, int cookiePrice, string ing1ID, string ing2ID, string cookieImage)
    {
        CookieID = cookieID;
        CookiePrice = cookiePrice;
        Ing1ID = ing1ID;
        Ing2ID = ing2ID;
        CookieImage = cookieImage;
    }
}
