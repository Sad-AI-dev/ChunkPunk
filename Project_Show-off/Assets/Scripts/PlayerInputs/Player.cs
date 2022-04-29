using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 toMove;
    [SerializeField] float moveSpeed = 5f;

    private void Start()
    {
        PlayerManager.instance.AddPlayer(this); //notify others of player's existance
    }

    public void SetMoveDir(Vector2 newToMove)
    {
        toMove = newToMove;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(toMove.x, 0, toMove.y) * (moveSpeed * Time.deltaTime);
    }
}
