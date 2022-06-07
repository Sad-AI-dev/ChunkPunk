using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryItem heldItem;
    [HideInInspector] public bool hasItem = false;

    //instantiation points
    public Transform placePoint, shootPoint;

    private void Update()
    {
        if (hasItem) {
            heldItem.Update();
        }
    }

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