using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public RectTransform batteryLevel;
    public Color regularColor;
    public Color emptyColor;

    private Vector2 originalSize;
    
    // Start is called before the first frame update
    void Start()
    {
        originalSize = batteryLevel.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChargeLevel(float chargePercent)
    {
        batteryLevel.sizeDelta = new Vector2(chargePercent * originalSize.x, originalSize.y);
        if(chargePercent <= 0f)
        {
            SetColor(emptyColor);
        }
    }

    public void SetColor(Color color)
    {
        GetComponent<RawImage>().color = color;
    }
}
