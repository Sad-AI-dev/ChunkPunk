using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 toMove;
    [SerializeField] float moveSpeed = 5f;

    [Header("technical settings")]
    [SerializeField] Emitter emitter;

    private void Start()
    {
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        toMove = newToMove;
    }

    public void Shoot()
    {
        List<GameObject> objs = emitter.Emit();
        foreach (GameObject obj in objs) {
            if (obj.TryGetComponent(out Projectile proj)) {
                proj.owner = this; //set owner of projectiles
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(toMove.x, 0, toMove.y) * (moveSpeed * Time.deltaTime);
    }
}
