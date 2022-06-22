using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInterfacer : MonoBehaviour
{
    [Header("Labels")]
    public TMP_Text scoreLabel;
    public TMP_Text stateLabel;

    [Header("Inventory")]
    public Image inventoryImage;
    public TMP_Text inventoryLabel;

    [Header("Groups")]
    public CanvasGroup stateGroup;
    public CanvasGroup mapGroup;

    [Header("Fader")]
    public Fader fader;
}
