using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MakeButtonBigger : MonoBehaviour
{
    RectTransform button;
    [SerializeField] int HeightIncrease;
    [SerializeField] int WidthIncrease;
    private void Awake()
    {
        button = GetComponent<RectTransform>();
    }
    public void makeBigger()
    {
        button.sizeDelta = new Vector2(button.rect.width + WidthIncrease, button.rect.height + HeightIncrease);
    }
    public void makeSmaller()
    {
        button.sizeDelta = new Vector2(button.rect.width - WidthIncrease, button.rect.height - HeightIncrease);
    }
}
