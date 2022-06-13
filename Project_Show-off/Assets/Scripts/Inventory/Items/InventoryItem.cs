using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ScriptableObject
{
    [SerializeField] int count = 1;
    public Sprite sprite;

    [Header("Technical")]
    [SerializeField] protected GameObject obstaclePrefab;
    [HideInInspector] public Inventory owner;

    public virtual void Initialize(Inventory inventory)
    {
        owner = inventory;
        owner.count = count;
    }

    public virtual void Update() { }

    //called when button is pressed down
    public virtual void OnSelect() { }

    //called when button is released
    public virtual void OnUse()
    {
        owner.count--;
        owner.SetUICount();
        if (owner.count <= 0) { 
            owner.hasItem = false;
            owner.SetUIImage(null);
        }
    }
}