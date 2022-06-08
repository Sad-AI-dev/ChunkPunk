using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    InventoryItem heldItem;
    [HideInInspector] public bool hasItem = false;
    [HideInInspector] public int count;

    //instantiation points
    public Transform placePoint, shootPoint;

    //UI
    Image targetImage;
    TMP_Text itemCount;


    //TEST
    private void Start()
    {
        InitializeUI();
    }

    private void Update()
    {
        if (hasItem) {
            heldItem.Update();
        }
    }

    public bool TryGetItem(InventoryItem item)
    {
        if (!hasItem) {
            heldItem = item;
            heldItem.Initialize(this);
            SetUIImage(heldItem.sprite);
            hasItem = true;
            return true; //succes
        }
        return false; //player already had item
    }

    public void SelectItem()
    {
        if (hasItem) {
            heldItem.OnSelect();
        }
    }

    public void UseItem()
    {
        if (hasItem) {
            heldItem.OnUse();
        }
    }

    //---------UI------------
    void InitializeUI()
    {
        UIInterfacer playerUI = PlayerManager.instance.playerUI[PlayerManager.instance.players.IndexOf(GetComponent<Player>())];
        targetImage = playerUI.inventoryImage;
        itemCount = playerUI.inventoryLabel;
    }

    public void SetUICount()
    {
        itemCount.text = count.ToString();
    }

    public void SetUIImage(Sprite sprite)
    {
        targetImage.sprite = sprite;
        targetImage.color = sprite == null ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
    }
}