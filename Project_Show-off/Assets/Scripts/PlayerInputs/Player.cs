using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float constantSpeed;
    bool isDecreasing;
    Vector2 toMove;
    Rigidbody playerBody;
    [SerializeField] float moveSpeed = 5f;
    float normalSpeed;
    [SerializeField] Vector3 m_EulerAngleVelocity;

    private void Start()
    {
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
        playerBody = GetComponent<Rigidbody>();
        normalSpeed = moveSpeed;
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        toMove = newToMove;
    }

    public void DecreaseSpeed()
    {
        isDecreasing = true;
    }
    public void IncreaseSpeed()
    {
        isDecreasing = false;
    }

    private void FixedUpdate()
    {
        if(isDecreasing && moveSpeed > constantSpeed)
        {
            moveSpeed--;
        } else if (!isDecreasing && moveSpeed < normalSpeed)
        {
            moveSpeed++;
        }




        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * toMove.x);
        Vector3 tempVec = new Vector3(0, 0, toMove.y) * (moveSpeed * Time.deltaTime);
        playerBody.velocity = transform.forward * Time.deltaTime * moveSpeed;
        if (playerBody.velocity.magnitude < 2.0f)
        {
            //playerBody.velocity = transform.forward * Time.deltaTime * constantSpeed;
        }
        playerBody.MoveRotation(playerBody.rotation * deltaRotation);
    }
}
