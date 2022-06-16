using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SOs/InventoryItems/PlaceableItem", order = 1)]
public class PlaceableItem : InventoryItem
{
    [SerializeField] GameObject previewPrefab;
    [SerializeField] float failShowDelay = 1f;

    [Header("Materials")]
    //materials
    Material previewMat;
    [SerializeField] Material failMat;
    MeshRenderer[] previewRenderer;

    Transform preview;
    CollisionDetector detector;
    bool isActive = false;
    bool frozen = false;

    public override void Initialize(Inventory inventory)
    {
        base.Initialize(inventory);
        SetupPreview(inventory);
    }
    void SetupPreview(Inventory inventory)
    {
        preview = Instantiate(previewPrefab, inventory.placePoint).transform;
        preview.gameObject.SetActive(false);
        //collisions detector
        detector = preview.GetComponent<CollisionDetector>();
        detector.onCollisionChanged.AddListener(UpdatePreview);
        //meshRenderer
        previewRenderer = preview.GetComponentsInChildren<MeshRenderer>();
        previewMat = previewRenderer[0].material;
    }

    public override void UpdateItem(Inventory inventory)
    {
        if (isActive) {
            //update preview pos?
        }
    }

    public override void OnSelect(Inventory inventory)
    {
        preview.gameObject.SetActive(true);
        isActive = true;
        UpdatePreview();
    }

    public override void OnUse(Inventory inventory)
    {
        if (isActive) {
            if (!detector.HasCollisions()) {
                PlaceObstacle();
                inventory.ConsumeItem();
            }
            else {
                inventory.CoroutineStarter(FailUseCo());
            }
        }
    }
    IEnumerator FailUseCo()
    {
        frozen = true;
        yield return new WaitForSeconds(failShowDelay);
        ResetPreview();
        frozen = false;
        isActive = false;
    }

    //--------------------place object------------------------
    void PlaceObstacle()
    {
        CreateObstacle();
        ResetPreview();
        isActive = false;
    }
    void CreateObstacle()
    {
        Transform t = Instantiate(obstaclePrefab).transform;
        t.SetPositionAndRotation(preview.position, preview.rotation);
    }

    //------------preview--------------
    void UpdatePreview()
    {
        if (!frozen) {
            if (detector.HasCollisions()) {
                if (IsPreviewMaterial()) { 
                    SetMaterial(failMat);
                }
            }
            else if (!detector.HasCollisions()) {
                if (!IsPreviewMaterial()) {
                    SetMaterial(previewMat);
                }
            }
        }
    }

    void ResetPreview()
    {
        preview.gameObject.SetActive(false);
    }

    //----------Materials----------
    void SetMaterial(Material mat)
    {
        foreach (MeshRenderer mesh in previewRenderer) {
            mesh.material = mat;
        }
    }

    bool IsPreviewMaterial()
    {
        return previewRenderer[0].material == previewMat;
    }
}
