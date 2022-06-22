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
    [SerializeField] float bulletDelay;
    [SerializeField] float accelerateIncrease;
    [SerializeField] float timeToSpeedUp;
    [SerializeField] float timeBetweenClick;
    [SerializeField] public ChangeTheFace faceChanger;
    float bulletClickCap;
    //result vectors
    Vector2 toMove;
    [HideInInspector] public Vector3 externalToMove = Vector3.zero;

    [Header("Camera Rotation settings")]
    [SerializeField] ChangeTheFace face;
    [SerializeField] Vector2 rotateSpeed;
    [SerializeField] float maxAimHeight = 2f;
    [SerializeField] float minAimHeight = -2f;
    [SerializeField] float leadEffectStrength = 2f;
    [SerializeField] float leadEffectSpeed = 10f;
    [SerializeField] float UTurnSpeed;
    [SerializeField] float UTurnAngle;
    [SerializeField] float accelerateMax;
    [SerializeField] float accelerateMin;
    Vector2 turnDirection;

    [Header("VFX")]
    [SerializeField] List<GameObject> speedParticles;

    [Header("Technical settings")]
    [SerializeField] Emitter emitter;
    private Vector3 lookAtStarter;
    public int id = 0;
    //cam points
    [HideInInspector] public GameObject neutralVCam;
    [HideInInspector] public GameObject aimVCam;
    public Transform LookAt;
    //leading camera vars
    [SerializeField] Transform baseLookAt;
    Vector3 baseLookAtPos;

    //visuals
    public Transform characterModel { get; private set; }

    public float accelerate;

    //states
    public bool isStunned = false;

    public bool isShoting;

    private bool isAccelerating;
    private bool isBraking;

    private bool isFast = false;

    private CanvasGroup mapGroup;

    //external components
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public GetHit getHit;
    [HideInInspector] public animationStateController stateController;


    private void Start()
    {
        //initialize values
        accelerate = 1;
        emitter.player = this;
        //lookats
        lookAtStarter = LookAt.localPosition;
        baseLookAtPos = baseLookAt.localPosition;
        //visuals
        characterModel = transform.GetChild(0);
        SetSpeedParticles(false);
        mapGroup = GetTargetGroup(this);
        //get external components
        rb = GetComponent<Rigidbody>();
        getHit = GetComponent<GetHit>();
        stateController = GetComponent<animationStateController>();
        //inventory
        inventory = GetComponent<Inventory>();
        inventory.Initialize();
        //notify others of player's existance
        PlayerManager.instance.AddPlayer(this);
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        toMove = newToMove;

        if(toMove.y > 0.3f && accelerate < accelerateMax)
        {
            StartCoroutine(increaseSpeed());
            isAccelerating = true;
        } 
        if(toMove.y < 0.3f )
        {
            //Debug.Log(isAccelerating);
            isAccelerating = false;
        }


        if(toMove.y < -0.3f && accelerate > accelerateMin)
        {
            StartCoroutine(decreaseSpeed());
            isBraking = true;
        } else if (toMove.y > -0.3f && accelerate < 1)
        {
            isBraking = false;
        }
    }
    
    public void ShowMap()
    {
        mapGroup.alpha = 1f;
    }
    public void HideMap()
    {
        mapGroup.alpha = 0f;
    }
    
    CanvasGroup GetTargetGroup(Player target)
    {
        return PlayerManager.instance.playerUI[target.id - 1].mapGroup;
    }
    private IEnumerator increaseSpeed()
    {
        accelerate += accelerateIncrease;
        yield return new WaitForSeconds(timeToSpeedUp);
        if (isAccelerating && accelerate < accelerateMax)
            StartCoroutine(increaseSpeed());
    }

    private IEnumerator decreaseSpeed()
    {
        accelerate -= accelerateIncrease;
        yield return new WaitForSeconds(timeToSpeedUp);
        if(isBraking && accelerate > accelerateMin)
        {
            StartCoroutine(decreaseSpeed());
        }
    }


    public void Look(Vector2 lookDir)
    {
        if (lookDir.magnitude > 1f) { lookDir.Normalize(); }
        turnDirection = lookDir;
    }

    public IEnumerator isShooting()
    {
        //Debug.Log("yes i shoot");
        if(bulletClickCap <= 0)
        {
            List<GameObject> objs = emitter.Emit();
            foreach (GameObject obj in objs)
            {
                Debug.Log(bulletClickCap);
                if (obj.TryGetComponent(out Projectile proj))
                {
                    //proj.owner = this; //set owner of projectiles
                }
                bulletClickCap = timeBetweenClick;

                yield return new WaitForSeconds(bulletDelay);
                if (isShoting)
                    StartCoroutine(isShooting());
            }

        }
        
    }


    public void UTurn() {
        //float rotationSpeed = 0.5f * 360f;
        //Debug.Log("UTurn");
        //Vector3 newRot = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        StartCoroutine(RotateObject(UTurnAngle, Vector3.up, UTurnSpeed));
    }
    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        while (true)
        {
            // save starting rotation position
            Quaternion startRotation = transform.rotation;

            float deltaAngle = 0;

            // rotate until reaching angle
            while (deltaAngle < angle)
            {
                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                yield return null;
            }

            // delay here
            break;
        }
    }

    public void stunned()
    {
        //stateController.stun?.Invoke();
    }

    public void Died()
    {
        this.transform.position = checkPointManager.instance.allPlayerCheckPoints[id].position;
    }
    public IEnumerator Accelerate(bool isAccelerating)
    {
        if (isAccelerating && accelerate < accelerateMax)
        {
            accelerate += 1;
            yield return new WaitForSeconds(1);
            //Debug.Log("speed up");
        }
        else if (!isAccelerating && accelerate > accelerateMin)
            accelerate -= 1;
        yield return new WaitForSeconds(1);
    }
    public IEnumerator Decelerate(bool isDecelerating)
    {
        if (isDecelerating && accelerate > accelerateMin)
        {
            accelerate -= 0.1f;
            yield return new WaitForSeconds(1);
        }
        else if (!isDecelerating && accelerate < accelerateMin)
            accelerate += 0.2f;
        yield return new WaitForSeconds(1);
    }



    private void FixedUpdate()
    {
        if (!isStunned)
        {
            Rotate();
            Move();
            //Debug.Log(accelerate);

            if (!isAccelerating && accelerate > 1)
            {
                //Debug.Log("Slow down!!");
                accelerate -= 0.1f;
            }
            else if (!isBraking && accelerate < 1)
                accelerate += 0.1f;
        }
        if (bulletClickCap >= 0)
            bulletClickCap -= Time.deltaTime;
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
        }
        //update lead effect
        UpdateBaseLookAt();
    }

    void UpdateBaseLookAt()
    {
        Vector3 targetOffset = new Vector3(turnDirection.normalized.x * leadEffectStrength, 0, 0);
        Vector3 targetPos = baseLookAtPos + targetOffset;
        baseLookAt.localPosition = Vector3.MoveTowards(baseLookAt.localPosition, targetPos, leadEffectSpeed * Time.deltaTime);
    }

    //---------------------Getters-------------------
    public float GetAccelerate()
    {
        return accelerate;
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
        Vector3 velocity = (transform.right * input.x) + (transform.forward * (input.y * accelerate) ) + (externalToMove * (100 * Time.deltaTime));
        rb.velocity = new Vector3(velocity.x, rb.velocity.y , velocity.z );
        //VFX
        GoFastCheck();
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

    private void GoFastCheck()
    {
        if (isFast && toMove.y < 0.1f) {
            //slow down
            isFast = false;
            SetSpeedParticles(false);
        }
        else if (!isFast && toMove.y > 0.1f) {
            //speed up
            isFast = true;
            SetSpeedParticles(true);
        }
    }

    private void SetSpeedParticles(bool state)
    {
        foreach (GameObject obj in speedParticles) {
            obj.SetActive(state);
        }
    }
}