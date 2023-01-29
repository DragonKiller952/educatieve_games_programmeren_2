using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public GameObject planetY;

    private Vector3 moveDirection;

    private Quaternion tempRotation;

    public float moveSpeed;
    public bool canMove;

    void Start()
    {
        canMove = true;
    }

    void Update()
    {
        // Capture the player movement input
        moveDirection = new Vector3(0, Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        // If player is allowed to move the planet
        if (canMove == true)
        {
            // Save Z rotation in case it goes over limit
            tempRotation = transform.rotation;

            // Rotate the Z and Y of the planet according to the input
            transform.Rotate(0, 0, moveDirection.z * moveSpeed / 100, Space.Self);
            planetY.transform.Rotate(0, moveDirection.y * moveSpeed / 100, 0, Space.Self);

            // Switch Z rotation back if it goes over the limits
            if (transform.rotation.z > 0.35f)
            {
                transform.rotation = tempRotation;
            }
            if (-0.20f > transform.rotation.z)
            {
                transform.rotation = tempRotation;
            }
        }
    }


}
