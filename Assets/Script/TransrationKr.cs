using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TransrationKr : MonoBehaviour
{
    public static TransrationKr instance;
    public string csvFilePath = "StringTableKr";

    private Dictionary<string, string> translations = new Dictionary<string, string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadTranslations();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadTranslations()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFilePath);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] row = line.Split(',');
                if (row.Length == 2)
                {
                    string key = row[0].Trim();
                    string value = row[1].Trim();
                    translations[key] = value;
                }
            }
        }
        else
        {
            Debug.LogError("Failed to load translation file from path: " + csvFilePath);
        }
    }

    public string GetTranslation(string key)
    {
        if (translations.TryGetValue(key, out string value))
        {
            return value;
        }
        return key;
    }
}
