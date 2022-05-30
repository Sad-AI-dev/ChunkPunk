using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Emitter : MonoBehaviour
{
    [SerializeField] private float shootWait;
    [SerializeField] GameObject emitPrefab;
    [SerializeField] List<Transform> emitLocations;
    [SerializeField] public Transform lookAt;
    private bool canShoot = true;

    [Header("Events")]
    [SerializeField] UnityEvent onEmit = new();

    public List<GameObject> Emit()
    {
        List<GameObject> objs = new();
        foreach (Transform t in emitLocations) {
            if (canShoot)
            {
                GameObject emitted = Instantiate(emitPrefab);
                emitted.transform.SetPositionAndRotation(t.position, t.rotation);
                //add to list
                objs.Add(emitted);
                StartCoroutine(timer());
            }
            
        }
        onEmit?.Invoke();
        return objs;
    }


    private IEnumerator timer()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootWait);
        canShoot = true;

    }

    private void FixedUpdate()
    {
        transform.LookAt(lookAt);
    }
}