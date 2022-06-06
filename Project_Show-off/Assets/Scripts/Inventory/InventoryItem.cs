using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SOs/InventoryItems/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    [SerializeField] int count = 1;
    [SerializeField] GameObject ObstaclePrefab;
    
    protected Inventory owner;

    public virtual void Initialize(Inventory inventory)
    {
        owner = inventory;
    }

    //called when button is pressed down
    public virtual void OnSelect()
    {

    }

    //called when button is released
    public virtual void OnUse()
    {
        count--;
        if (count <= 0) { owner.hasItem = false; }
    }
}