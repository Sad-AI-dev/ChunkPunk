using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SOs/InventoryItems/PlaceableItem", order = 1)]
public class PlaceableItem : InventoryItem
{
    [SerializeField] GameObject previewPrefab;
    [SerializeField] float failShowDelay = 1f;

    [Header("Technical")]
    [SerializeField] float groundCheckDistance = 1f;

    //materials
    [Header("Materials")]
    Material previewMat;
    [SerializeField] Material failMat;
    MeshRenderer[] previewRenderer;

    Transform preview;
    CollisionDetector detector;

    //states
    bool isActive = false;
    bool grounded = false;
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
            //UpdatePreviewPos(inventory);
        }
    }
    void UpdatePreviewPos(Inventory inventory)
    {
        Ray ray = new Ray(inventory.placePoint.position + new Vector3(0, 1f, 0), Vector3.down);
        grounded = Physics.Raycast(ray, out RaycastHit hit, groundCheckDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
        if (grounded) {
            //position
            preview.position = hit.point;
            //rotation
            //preview.up = hit.normal;
            //preview.localEulerAngles = new Vector3(preview.localEulerAngles.x, 0, preview.localEulerAngles.z);
            preview.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * Quaternion.LookRotation(hit.normal);
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
            else if (!IsPreviewMaterial()) {
                SetMaterial(previewMat);
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
