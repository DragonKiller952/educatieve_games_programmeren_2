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
        //var cpos = camera.transform.position;
        //cpos.y = transform.rotation.y;
        //var direction = (transform.position - cpos).normalized;
        //var forward = direction * Input.GetAxis("Vertical");
        //var right = Vector3.Cross(Vector3.up, direction) * Input.GetAxis("Horizontal");
        //var moveDirection = (forward + right).normalized;

        //moveDirection = new Vector3(0, Input.GetAxisRaw("Horizontal"), 0) + (((transform.rotation) * Vector3.forward) * -Input.GetAxisRaw("Vertical"));
        moveDirection = new Vector3(0, Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")).normalized;
        //print(transform.rotation);
        //rotation = transform.rotation * Quaternion.Euler(moveDirection);
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            tempRotation = transform.rotation;
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * moveSpeed);
            transform.Rotate(0, 0, moveDirection.z * moveSpeed / 100, Space.Self);
            planet.transform.Rotate(0, moveDirection.y * moveSpeed / 100, 0, Space.Self);
            //print((transform.rotation.x, transform.rotation.z, transform.rotation.x+transform.rotation.z));
            //print(transform.rotation.z);
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
