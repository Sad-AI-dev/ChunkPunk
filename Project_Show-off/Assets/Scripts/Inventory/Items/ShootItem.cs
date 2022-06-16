using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SOs/InventoryItems/ShootItem", order = 2)]
public class ShootItem : InventoryItem
{
    public override void OnSelect(Inventory inventory)
    {
        CreateItem(inventory);
        inventory.ConsumeItem();
    }

    void CreateItem(Inventory inventory)
    {
        Transform t = Instantiate(obstaclePrefab).transform;
        t.SetPositionAndRotation(inventory.shootPoint.position, inventory.shootPoint.rotation);
    }
}
