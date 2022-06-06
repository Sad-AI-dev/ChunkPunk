using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryItem heldItem;
    public bool hasItem = false;

    public bool TryGetItem(InventoryItem item)
    {
        if (!hasItem) {
            //TODO update UI
            heldItem = item;
            heldItem.Initialize(this);
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
}