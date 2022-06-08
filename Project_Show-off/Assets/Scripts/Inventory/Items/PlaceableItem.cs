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

    public override void Initialize(Inventory inventory)
    {
        base.Initialize(inventory);
        SetupPreview();
    }
    void SetupPreview()
    {
        preview = Instantiate(previewPrefab, owner.placePoint).transform;
        preview.gameObject.SetActive(false);
        //collisions detector
        detector = preview.GetComponent<CollisionDetector>();
        //meshRenderer
        previewRenderer = preview.GetComponentsInChildren<MeshRenderer>();
        previewMat = previewRenderer[0].material;
    }

    public override void Update()
    {
        if (isActive) {
            //update preview pos?
        }
    }

    public override void OnSelect()
    {
        preview.gameObject.SetActive(true);
        isActive = true;
    }

    public override void OnUse()
    {
        if (isActive) {
            if (!detector.HasCollisions()) {
                base.OnUse();
                PlaceObstacle();
            }
            else {
                owner.CoroutineStarter(FailUseCo());
            }
        }
    }
    IEnumerator FailUseCo()
    {
        SetMaterial(failMat);
        yield return new WaitForSeconds(failShowDelay);
        ResetPreview();
        isActive = false;
    }

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
    void ResetPreview()
    {
        preview.gameObject.SetActive(false);
        SetMaterial(previewMat);
    }

    //----------Materials----------
    void SetMaterial(Material mat)
    {
        foreach (MeshRenderer mesh in previewRenderer) {
            mesh.material = mat;
        }
    }
}
