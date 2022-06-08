using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryItem heldItem;
    public bool hasItem = false;
    [HideInInspector] public int count;

    //instantiation points
    public Transform placePoint, shootPoint;

    //UI
    [HideInInspector] public Image targetImage;

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

    //---------UI------------
    void GetTargetImage()
    {
        GameObject playerUI = PlayerManager.instance.playerUI[PlayerManager.instance.players.IndexOf(GetComponent<Player>())];
        
    }
}