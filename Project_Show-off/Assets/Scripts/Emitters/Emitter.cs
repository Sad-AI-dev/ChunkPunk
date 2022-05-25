using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Emitter : MonoBehaviour
{
    [SerializeField] GameObject emitPrefab;
    [SerializeField] List<Transform> emitLocations;
    [SerializeField] public Transform lookAt;

    [Header("Events")]
    [SerializeField] UnityEvent onEmit = new();

    public List<GameObject> Emit()
    {
        List<GameObject> objs = new();
        foreach (Transform t in emitLocations) {
            GameObject emitted = Instantiate(emitPrefab);
            emitted.transform.SetPositionAndRotation(t.position, t.rotation);
            //add to list
            objs.Add(emitted);
        }
        onEmit?.Invoke();
        return objs;
    }

    private void FixedUpdate()
    {
        transform.LookAt(lookAt);
    }
}