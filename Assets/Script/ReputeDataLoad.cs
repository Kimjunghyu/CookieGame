using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReputeDataLoad : MonoBehaviour
{
    public static ReputeDataLoad instance;
    public string csvFilePath = "Assets/Resources/Table/ReputeTable.csv";

    private List<ReputeData> reputeDataList = new List<ReputeData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadReputeData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadReputeData()
    {
        if (File.Exists(csvFilePath))
        {
            string[] lines = File.ReadAllLines(csvFilePath);

            bool isFirstLine = true;
            foreach (string line in lines)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }
                if (string.IsNullOrEmpty(line)) continue;
                string[] row = line.Split(','); // Assuming the CSV is tab-separated
                if (row.Length >= 6)
                {
                    int id = int.Parse(row[0]);
                    int start = int.Parse(row[1]);
                    int end = int.Parse(row[2]);
                    int visitStart = int.Parse(row[3]);
                    int visitEnd = int.Parse(row[4]);
                    int tax = int.Parse(row[5]);
                    reputeDataList.Add(new ReputeData(id, start, end, visitStart, visitEnd, tax));
                }
            }

            Debug.Log("Loaded Repute Data: " + reputeDataList.Count + " entries.");
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + csvFilePath);
        }
    }

    public ReputeData GetReputeData(int repute)
    {
        foreach (var data in reputeDataList)
        {
            if (repute >= data.ReputeStStart && repute <= data.ReputeStEnd)
            {
                return data;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ReputeData
{
    public int ReputeID;
    public int ReputeStStart;
    public int ReputeStEnd;
    public int CusVisitTimerStart;
    public int CusVisitTimerEnd;
    public int Tax;

    public ReputeData(int id, int start, int end, int visitStart, int visitEnd, int tax)
    {
        ReputeID = id;
        ReputeStStart = start;
        ReputeStEnd = end;
        CusVisitTimerStart = visitStart;
        CusVisitTimerEnd = visitEnd;
        Tax = tax;
    }
}
