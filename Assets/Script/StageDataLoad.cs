using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataLoad : MonoBehaviour
{
    public static StageDataLoad instance;
    public string csvFilePath = "StageTable"; // 확장자를 제외한 Resources 폴더 내부 경로

    private List<string[]> stageDataRows = new List<string[]>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStageData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadStageData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFilePath);

        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

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
                stageDataRows.Add(row);
            }
        }
        else
        {
            Debug.LogError("Failed to load data from StageTable at path: " + csvFilePath);
        }
    }

    public StageData GetStageData(int stage)
    {
        if (stage >= 0 && stage < stageDataRows.Count)
        {
            string[] row = stageDataRows[stage];
            if (row.Length > 3)
            {
                int stageNumber = int.Parse(row[0]);
                int cookieStart = int.Parse(row[1]);
                int cookieEnd = int.Parse(row[2]);
                int stageTimer = int.Parse(row[3]);
                return new StageData(stageNumber, cookieStart, cookieEnd, stageTimer);
            }
        }
        return null;
    }
}

[System.Serializable]
public class StageData
{
    public int stage;
    public int cookieStart;
    public int cookieEnd;
    public int stageTimer;

    public StageData(int stage, int cookieStart, int cookieEnd, int stageTimer)
    {
        this.stage = stage;
        this.cookieStart = cookieStart;
        this.cookieEnd = cookieEnd;
        this.stageTimer = stageTimer;
    }
}
