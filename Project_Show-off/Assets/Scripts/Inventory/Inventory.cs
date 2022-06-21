using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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

    //external components
    [SerializeField] private AudioSource source;
    public Emitter emitter;

    private void Start()
    {
        //reset items
        hasItem = false;
        heldItem = null;
    }

    public void Initialize()
    {
        InitializeUI();
    }

    private void Update()
    {
        if (hasItem) {
            heldItem.UpdateItem(this);
        }
    }

    public bool TryGetItem(InventoryItem item)
    {
        if (!hasItem) {
            heldItem = item;
            heldItem.Initialize(this);
            SetUICount();
            SetUIImage(heldItem.sprite);
            hasItem = true;
            return true; //succes
        }
        return false; //player already had item
    }

    public void SelectItem()
    {
        if (hasItem) {
            heldItem.OnSelect(this);
        }
    }

    public void UseItem()
    {
        if (hasItem) {
            heldItem.OnUse(this);
        }
    }

    public void ConsumeItem()
    {
        PlaySound();
        count--;
        SetUICount();
        if (count <= 0) { 
            hasItem = false;
            SetUIImage(null);
        }
    }

    //-----------Sound------------
    void PlaySound()
    {
        if (source.clip != heldItem.clip) {
            source.clip = heldItem.clip;
        }
        source.Play();
    }

    //---------UI------------
    void InitializeUI()
    {
        UIInterfacer playerUI = PlayerManager.instance.playerUI[GetComponent<Player>().id - 1];
        targetImage = playerUI.inventoryImage;
        itemCount = playerUI.inventoryLabel;
    }

    public void SetUICount()
    {
        itemCount.text = count.ToString();
        itemCount.alpha = count <= 1 ? 0 : 255; //hide text if 1 or less items left
    }

    public void SetUIImage(Sprite sprite)
    {
        targetImage.sprite = sprite;
        targetImage.color = sprite == null ? new Color(0, 0, 0, 0) : new Color(255, 255, 255, 255);
    }

    //--------------util-----------
    public void CoroutineStarter(IEnumerator coRoutine)
    {
        StartCoroutine(coRoutine);
    }
}