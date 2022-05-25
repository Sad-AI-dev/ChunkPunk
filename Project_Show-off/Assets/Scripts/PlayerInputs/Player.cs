using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float moveSpeedX = 1f;
    [SerializeField] float forwardForce = 4;
    [SerializeField] float minSpeed = 3f;
    [SerializeField] float maxSpeed = 5f;
    //result vectors
    Vector2 toMove;
    [HideInInspector] public Vector3 externalToMove = Vector3.zero;

    [Header("Camera Rotation settings")]
    [SerializeField] Vector2 rotateSpeed;
    [SerializeField] float maxAimHeight = 2f;
    [SerializeField] float minAimHeight = -2f;
    Vector2 turnDirection;

    [Header("Technical settings")]
    [SerializeField] Emitter emitter;
    private Vector3 lookAtStarter;
    public int id = 0;
    //cam points
    [HideInInspector] public GameObject neutralVCam;
    [HideInInspector] public GameObject aimVCam;
    public Transform LookAt;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
        lookAtStarter = LookAt.localPosition;
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        toMove = newToMove;
    }

    public void Look(Vector2 lookDir)
    {
        if (lookDir.magnitude > 1f) { lookDir.Normalize(); }
        turnDirection = lookDir;
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

    public void Died()
    {
        //Debug.Log("Died");
        //int ID = this.id;
        //Debug.Log(ID);
        //transform.position = checkPointManager.instance.allPlayerCheckPoints[id].position;
        //Debug.Log("checkpoint is " + checkPointManager.instance.allPlayerCheckPoints[id].position);
        //Debug.Log(transform.position);
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    //------------------------rotation----------------------------
    void Rotate()
    {
        //x rotation
        transform.Rotate(new Vector3(0, turnDirection.x, 0) * (rotateSpeed.x * Time.deltaTime));
        //rb.rotation = Quaternion.Euler(new Vector3(0, 20000, 0) * Time.deltaTime);

        if (aimVCam.activeInHierarchy)
        {
            //y rotation (only seen when aiming)
            LookAt.localPosition += new Vector3(0, turnDirection.y, 0) * (rotateSpeed.y * Time.deltaTime);
            float clampedPos = Mathf.Clamp(LookAt.localPosition.y, minAimHeight, maxAimHeight); //clamp rotation
            LookAt.localPosition = new Vector3(LookAt.localPosition.x, clampedPos, LookAt.localPosition.z);
        } else
        {
            LookAt.localPosition = lookAtStarter;
        }
        
    }

    public void Banana(Vector3 direction)
    {
        Debug.Log("banaan");
        //rb.AddForceAtPosition(direction * 100000, transform.position);
    }

    //---------------aiming------------------
    public void Aim(bool isAimed)
    {
        aimVCam.SetActive(isAimed);
        neutralVCam.SetActive(!isAimed);
    }

    //--------------------------movement----------------------------------
    void Move()
    {
        Vector2 input = GetInputVelocity();
        Vector3 velocity = (transform.right * input.x) + (transform.forward * input.y) + (externalToMove * (100 * Time.deltaTime));
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    Vector2 GetInputVelocity()
    {
        Vector2 input = new(toMove.x * moveSpeedX, GetYInput());
        return input * (100 * Time.deltaTime);
    }
    float GetYInput()
    {
        float yInput = toMove.y * (toMove.y > 0 ? maxSpeed : minSpeed);
        return yInput + forwardForce;
    }
}