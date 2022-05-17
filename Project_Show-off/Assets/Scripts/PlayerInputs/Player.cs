using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public GameObject player1Cam;
    [SerializeField] private GameObject player2Cam;
    [SerializeField] private GameObject player1AimCam;
    [SerializeField] private GameObject player2AimCam;
    private int turnDirection;
    bool isSlippy;
    bool isInteracting;
    Vector2 toMove;
    [SerializeField] float moveSpeed;
    private bool isSlowing;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float minimumSpeed;
    [SerializeField] private float normalSpeed;
    Rigidbody rb;
    Vector3 rampDirect;
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
        while(isAccelerating && moveSpeed < maximumSpeed && !isInteracting)
        {
            moveSpeed++;
        }
        while(!isAccelerating && moveSpeed > normalSpeed && !isInteracting)
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

    public void Look(int newDirection)
    {
        turnDirection = newDirection;
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

        if(other.gameObject.tag == "Ramp")
        {
            Ramp ramp = other.GetComponent<Ramp>();
            rampDirect = ramp.rampDirection;
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
     if(other.gameObject.tag == "Ramp")
        {
            rampDirect = new Vector3(0, 0, 0);
        }
    }
    private void FixedUpdate()
    {

        transform.Rotate(new Vector3(0, turnDirection, 0) * turnSpeed * Time.deltaTime);
        while (isSlowing && moveSpeed > minimumSpeed)
        {
            moveSpeed--;
        }
        while(!isSlowing && moveSpeed < normalSpeed)
        {
            moveSpeed++;
        }
        Vector3 direction = transform.forward * (moveSpeed) + rampDirect;
        //transform.position += ;
        //transform.position += new Vector3(toMove.x, 0, toMove.y) * (Time.deltaTime);
        //Debug.Log(moveSpeed);
        rb.AddForce(direction);
    }
    public void Aim(bool isAimed)
    {
        if (id == 1)
        {

            Debug.Log("aiming");
            player1AimCam.SetActive(isAimed);
            player1Cam.SetActive(!isAimed);

        }
        else if (id == 2)
        {

            player2AimCam.SetActive(isAimed);
            player2Cam.SetActive(!isAimed);

        }
    }
}
