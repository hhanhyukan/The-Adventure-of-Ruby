using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    // Create static property to take reference at any where
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    float originalSize;

    // Run this as soon as the game start
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
