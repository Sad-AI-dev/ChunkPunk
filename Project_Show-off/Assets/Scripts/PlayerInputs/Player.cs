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
    public int id = 0;
    //cam points
    [HideInInspector] public GameObject neutralVCam;
    [HideInInspector] public GameObject aimVCam;
    public Transform LookAt;
    public bool isStunned = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
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
        Debug.Log("ateast ehre");
        //Debug.Log(ID);
        this.transform.position = checkPointManager.instance.allPlayerCheckPoints[this].position;
        Debug.Log("transfor is " + transform.position);
        //Debug.Log("checkpoint is " + checkPointManager.instance.allPlayerCheckPoints[id].position);
        //Debug.Log(transform.position);
        //transform.position = checkPointManager.instance.allPlayerCheckPoints[id].position;
        //transform.position = checkPointManager.instance.allPlayerCheckPoints[this];
    }

    private void FixedUpdate()
    {

        if (!isStunned)
        {
            Rotate();
            Move();
        }
        
    }

    //------------------------rotation----------------------------
    void Rotate()
    {
        //x rotation
        transform.Rotate(new Vector3(0, turnDirection.x, 0) * (rotateSpeed.x * Time.deltaTime));

        //y rotation (only seen when aiming)
        LookAt.position += new Vector3(0, turnDirection.y, 0) * (rotateSpeed.y * Time.deltaTime);
        float clampedPos = Mathf.Clamp(LookAt.position.y, minAimHeight, maxAimHeight); //clamp rotation
        LookAt.position = new Vector3(LookAt.position.x, clampedPos, LookAt.position.z);
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