using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 turnDirection;
    bool isSlippy;
    private bool isSlowing;
    [SerializeField] float moveSpeed;
    [SerializeField] int Slippyness;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float minimumSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float yAxisResistance;
    Rigidbody rb;
    Vector3 rampDirect;

    [Header("technical settings")]
    [SerializeField] Emitter emitter;
    public int id = 0;
    //cam points
    [HideInInspector] public GameObject neutralVCam;
    [HideInInspector] public GameObject aimVCam;
    [SerializeField] public GameObject LookAt;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
    }

    public void Slowing(bool isSlowed)
    {
        isSlowing = isSlowed;
    }

    public void Accelerating(bool isAccelerating)
    {
        if (isAccelerating) { moveSpeed = maximumSpeed; }
        else { moveSpeed = normalSpeed; }
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        Accelerating(newToMove.y > 0.2f);
        Slowing(newToMove.y < -0.2f);
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

    public void Look(Vector2 lookDir)
    {
        if(lookDir.x < -0.3 || lookDir.x > 0.3 || lookDir.y < -0.3 || lookDir.y > 0.3) { turnDirection = lookDir.normalized; }
        else { turnDirection = Vector2.zero; }
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
        transform.Rotate(new Vector3(0, turnDirection.x, 0) * (turnSpeed * Time.deltaTime));
        //slowing speed
        if(LookAt.transform.localPosition.y < 7 && LookAt.transform.localPosition.y > 1)
        {
            LookAt.transform.position += new Vector3(0, turnDirection.y / yAxisResistance, 0);
        } 
        if (LookAt.transform.localPosition.y > 7)
        {
            LookAt.transform.localPosition = new Vector3(LookAt.transform.localPosition.x,  6.8f, LookAt.transform.localPosition.z);
        }
        
        if (LookAt.transform.localPosition.y < 1)
        {
            LookAt.transform.localPosition += new Vector3(LookAt.transform.localPosition.x, 1.2f, LookAt.transform.localPosition.z);
        }

        if (isSlowing) { moveSpeed = minimumSpeed; }
        else if (moveSpeed < normalSpeed) { moveSpeed = normalSpeed; }
        //add final force
        Vector3 direction = transform.forward * (moveSpeed * 100 * Time.deltaTime) + rampDirect;
        rb.AddForce(direction);
    }

    public void Aim(bool isAimed)
    {
        aimVCam.SetActive(isAimed);
        neutralVCam.SetActive(!isAimed);
    }
}
