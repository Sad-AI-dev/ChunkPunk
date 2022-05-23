using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Ramp : MonoBehaviour
{
    [SerializeField] Vector3 rampForce = Vector3.zero; //local space
    Vector3 playerForce; //world space

    readonly List<Player> effectedPlayers = new();

    private void Start()
    {
        playerForce = GetPlayerForce();
    }
    Vector3 GetPlayerForce()
    {
        return transform.right * rampForce.x + transform.up * rampForce.y + transform.forward * rampForce.z;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (other.TryGetComponent(out Player p)) {
                if (!effectedPlayers.Contains(p)) {
                    effectedPlayers.Add(p);
                    p.externalToMove += playerForce;
                    Debug.Log("added player");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (other.TryGetComponent(out Player p)) {
                if (effectedPlayers.Contains(p)) {
                    effectedPlayers.Remove(p);
                    p.externalToMove -= playerForce;
                }
            }
        }
    }
}
