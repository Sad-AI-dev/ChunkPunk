using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float constantSpeed;
    Vector2 toMove;
    Rigidbody playerBody;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 m_EulerAngleVelocity;

    private void Start()
    {
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
        playerBody = GetComponent<Rigidbody>();
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        toMove = newToMove;
    }

    private void FixedUpdate()
    {
        //transform.position += new Vector3(toMove.x, 0, toMove.y) * (moveSpeed * Time.deltaTime);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * toMove.x);
        playerBody.AddForce(new Vector3(0, 0, toMove.y) * (moveSpeed * Time.deltaTime));

        if(playerBody.velocity.magnitude < 2.0f)
        {
            //playerBody.AddForce(new Vector3(0, 0, constantSpeed));
        }
        playerBody.MoveRotation(playerBody.rotation * deltaRotation);
    }
}
