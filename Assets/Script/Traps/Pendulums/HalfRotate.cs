using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRotate : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public float leftAngle;
    public float rightAngle;
    bool movingClockwise;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingClockwise = true;
    }
    void Update()
    {
        rb.angularVelocity = moveSpeed;
        Move();

    }

    private void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }
    }
    private void Move()
    {
        ChangeMoveDir();

        if (movingClockwise)
        {
            rb.angularVelocity = moveSpeed;
        }
        if (!movingClockwise)
        {
            rb.angularVelocity = -1*moveSpeed;
        }
    }
}
