using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Toggle autoToggle;
    public Toggle manualToggle;
    public CookieManager cookieManager;

    private const string AutoToggleKey = "AutoToggle";
    private const string ManualToggleKey = "ManualToggle";

    private void Start()
    {
        autoToggle.isOn = PlayerPrefs.GetInt(AutoToggleKey, 1) == 1;
        manualToggle.isOn = PlayerPrefs.GetInt(ManualToggleKey, 0) == 1;

        if (autoToggle.isOn)
        {
            cookieManager.SetSelectionMode(CookieManager.SelectionMode.Auto);
        }
        else if (manualToggle.isOn)
        {
            cookieManager.SetSelectionMode(CookieManager.SelectionMode.Select);
        }
    }


    public void OnToggleChanged(bool autoSelected)
    {
        if (autoSelected)
        {
            PlayerPrefs.SetInt(AutoToggleKey, 1);
            PlayerPrefs.SetInt(ManualToggleKey, 0);
        }
        else
        {
            PlayerPrefs.SetInt(AutoToggleKey, 0);
            PlayerPrefs.SetInt(ManualToggleKey, 1);
        }
        PlayerPrefs.Save();

        if (autoToggle.isOn)
        {
            cookieManager.SetSelectionMode(CookieManager.SelectionMode.Auto);
        }
        else if (manualToggle.isOn)
        {
            cookieManager.SetSelectionMode(CookieManager.SelectionMode.Select);
        }
    }
}
