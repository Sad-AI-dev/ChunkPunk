using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttonincrease : MonoBehaviour
{
    RectTransform thisOne;
    [SerializeField] int ButtonHeightIncrease;
    [SerializeField] int ButtonWidthIncrease;
    private void Awake()
    {
        thisOne = this.GetComponent<RectTransform>();
    }
    public void Increase()
    {
        thisOne.sizeDelta = new Vector2(thisOne.rect.width + ButtonWidthIncrease, thisOne.rect.height + ButtonHeightIncrease);
    }
    public void Decrease()
    {
        thisOne.sizeDelta = new Vector2(thisOne.rect.width - ButtonWidthIncrease, thisOne.rect.height - ButtonHeightIncrease);
    }
}
