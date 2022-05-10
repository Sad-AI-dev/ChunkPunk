using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField] GameObject emitPrefab;
    [SerializeField] List<Transform> emitLocations;

    public List<GameObject> Emit()
    {
        List<GameObject> objs = new();
        foreach (Transform t in emitLocations) {
            GameObject emitted = Instantiate(emitPrefab);
            emitted.transform.SetPositionAndRotation(t.position, t.rotation);
            //add to list
            objs.Add(emitted);
        }
        return objs;
    }
}