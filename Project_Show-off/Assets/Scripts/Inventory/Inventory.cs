using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryItem heldItem;
    public bool hasItem = false;
    [HideInInspector] public int count;

    //instantiation points
    public Transform placePoint, shootPoint;

    //TEST
    private void Start()
    {
        if (hasItem) { heldItem.Initialize(this); }
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