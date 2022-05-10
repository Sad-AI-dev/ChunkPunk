using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 toMove;
    [SerializeField] float moveSpeed = 7f;
    private bool isSlowing;
    private float maximumSpeed;
    private float minimumSpeed = 3f;
    [Header("technical settings")]
    [SerializeField] Emitter emitter;

    private void Start()
    {
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
        maximumSpeed = moveSpeed;
    }

    public void Slowing(bool isSlowed)
    {
        if (isSlowed)
        {
            isSlowing = true;
            Debug.Log("Slowing");
        } else
        {
            isSlowing = false;
        }
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

    public void Look(Vector2 newLook)
    {
        //Vector3 lookVec = new Vector3(0, newLook.x, 0);
        transform.Rotate(0, newLook.x, 0);
        Debug.Log("Looking");
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * (moveSpeed * Time.deltaTime);


        
        while(isSlowing && moveSpeed > minimumSpeed)
        {
            moveSpeed--;
        }
        while(!isSlowing && moveSpeed < maximumSpeed)
        {
            moveSpeed++;
        }

        
         transform.position += new Vector3(toMove.x, 0, toMove.y) * (Time.deltaTime);
        
    }
}
