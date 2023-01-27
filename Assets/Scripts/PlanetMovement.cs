using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public float moveSpeed;

    public GameObject planet;

    private Vector3 moveDirection;

    private Quaternion tempRotation;

    public bool canMove = true;

   

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(0, Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            tempRotation = transform.rotation;

            transform.Rotate(0, 0, moveDirection.z * moveSpeed / 100, Space.Self);
            planet.transform.Rotate(0, moveDirection.y * moveSpeed / 100, 0, Space.Self);

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
