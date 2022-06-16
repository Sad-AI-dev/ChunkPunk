using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ScriptableObject
{
    [SerializeField] int count = 1;
    public Sprite sprite;

    [Header("Technical")]
    [SerializeField] protected GameObject obstaclePrefab;
    //[HideInInspector] public Inventory owner;

    public virtual void Initialize(Inventory inventory)
    {
        inventory.count = count;
    }

    public virtual void UpdateItem(Inventory inventory) { }

    //called when button is pressed down
    public virtual void OnSelect(Inventory inventory) { }

    //called when button is released
    public virtual void OnUse(Inventory inventory) { }
}