using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 toMove;
    [SerializeField] float moveSpeed = 7f;
    private bool isSlowing;
    private float maximumSpeed = 10;
    private float minimumSpeed = 3f;
    private float normalSpeed = 7;
    [Header("technical settings")]
    [SerializeField] Emitter emitter;
    public int id = 0;

    private void Start()
    {
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
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

    public void Accelerating(bool isAccelerating)
    {
        while(isAccelerating && moveSpeed < maximumSpeed)
        {
            moveSpeed++;
        }
        while(!isAccelerating && moveSpeed > normalSpeed)
        {
            moveSpeed--;
            Debug.Log("notaccelerating");
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
        //Debug.Log("Looking");
    }
    public void Died()
    {
        Debug.Log("Died");
        int ID = this.id;
        Debug.Log(ID);
        transform.position = checkPointManager.instance.allPlayerCheckPoints[id].position;
        Debug.Log("checkpoint is " + checkPointManager.instance.allPlayerCheckPoints[id].position);
        Debug.Log(transform.position);
    }
    private void FixedUpdate()
    {



        while (isSlowing && moveSpeed > minimumSpeed)
        {
            moveSpeed--;
        }
        while(!isSlowing && moveSpeed < normalSpeed)
        {
            moveSpeed++;
        }

        transform.position += transform.forward * (moveSpeed * Time.deltaTime);
         //transform.position += new Vector3(toMove.x, 0, toMove.y) * (Time.deltaTime);
        
    }
}
