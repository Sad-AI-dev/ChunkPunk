using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ScriptableObject
{
    [SerializeField] int count = 1;
    [SerializeField] Sprite sprite;

    [Header("Technical")]
    [SerializeField] protected GameObject obstaclePrefab;
    
    protected Inventory owner;

    public virtual void Initialize(Inventory inventory)
    {
        owner = inventory;
        inventory.count = count;
    }

    public virtual void Update()
    {

    }

    //called when button is pressed down
    public virtual void OnSelect()
    {

    }

    //called when button is released
    public virtual void OnUse()
    {
        owner.count--;
        if (owner.count <= 0) { owner.hasItem = false; }
    }
}