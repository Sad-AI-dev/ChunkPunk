using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isSlippy;
    Vector2 toMove;
    [SerializeField] float moveSpeed;
    private bool isSlowing;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float minimumSpeed;
    [SerializeField] private float normalSpeed;
    Rigidbody rb;

    [Header("technical settings")]
    [SerializeField] Emitter emitter;
    [SerializeField] int Slippyness;
    [SerializeField] float turnSpeed = 1f;
    public int id = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        transform.Rotate(new Vector3(0f, newLook.x, 0f) * (turnSpeed));
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


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Slippery" && isSlippy == false)
        {
            Debug.Log("Puddle!");
            //moveSpeed = (normalSpeed * Slippyness);
            //isSlippy = true;
            //turnSpeed *= 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
     if(isSlippy == true)
        {
            moveSpeed = normalSpeed;
            turnSpeed = 1;
            isSlippy = false;
        }   
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
        Vector3 direction = transform.forward * (moveSpeed );
        //transform.position += ;
        //transform.position += new Vector3(toMove.x, 0, toMove.y) * (Time.deltaTime);
        Debug.Log(moveSpeed);
        rb.AddForce(direction);
    }
}
