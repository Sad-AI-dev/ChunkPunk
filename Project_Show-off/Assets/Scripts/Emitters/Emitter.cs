using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Emitter : MonoBehaviour
{
    [SerializeField] private float shootWait;
    public List<GameObject> emitPrefab;
    [SerializeField] List<Transform> emitLocations;
    [SerializeField] public Transform lookAt;
    public Player player;
    private bool canShoot = true;

    [Header("Events")]
    [SerializeField] UnityEvent onEmit = new();

    public List<GameObject> Emit()
    {
        List<GameObject> objs = new();
        if (canShoot) {
            foreach (Transform t in emitLocations) {
                int emit = Random.Range(0, emitPrefab.Count );
                GameObject emitted = Instantiate(emitPrefab[emit]);

                emitted.transform.SetPositionAndRotation(t.position, t.rotation);
                //add to list
                objs.Add(emitted);
                StartCoroutine(timer());
            }
            onEmit?.Invoke();
            CoinManager.instance.ChargeMoney(this.player, 1);
        }
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


        //if(CoinManager.instance.bullets[this.player] < 1)
        //{
        //    canShoot = false;
        //} else
        //{
        //    canShoot = true;
        //}
    }
}