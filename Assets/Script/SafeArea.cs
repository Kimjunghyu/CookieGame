using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public RectTransform safeAreaRect;

    void Start()
    {
        if (safeAreaRect == null)
        {
            safeAreaRect = GetComponent<RectTransform>();
        }
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        safeAreaRect.anchorMin = anchorMin;
        safeAreaRect.anchorMax = anchorMax;
    }
}
