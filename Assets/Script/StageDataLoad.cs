using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageDataLoad : MonoBehaviour
{
    public static StageDataLoad instance;
    public string csvFilePath = "Assets/Resources/Table/StageTable.csv";

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
                string[] row = line.Split(',');
                stageDataRows.Add(row);
            }
        }
        else
        {
            Debug.LogError("CSV 파일 불러오기 실패 :" + csvFilePath);
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
                Debug.Log($"스테이지 데이터 가져오기 성공: {stageNumber}");
                return new StageData(stageNumber, cookieStart, cookieEnd, stageTimer);
            }
        }
        Debug.LogError("유효한 데이터가 없습니다: " + stage);
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