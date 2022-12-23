using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public GameObject planetX;

    public GameObject planetY;

    public GameObject pin;

    private Vector3 moveDirection;

    // Update is called once per frame
    void Update()
    {
        moveDirection = (pin.transform.position - transform.position).normalized;
        //print((pin.transform.position, transform.position, pin.transform.position - transform.position, Vector3.Distance(transform.position, pin.transform.position)));
        //moveDirection.y = 0;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, pin.transform.position) > 0.5)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + moveDirection * moveSpeed * Time.deltaTime);
            //transform.LookAt(new Vector3(pin.transform.position.x, transform.position.y, pin.transform.position.z));

            var plane = new Plane(transform.up, transform.position);
            var mappedTargetPosition = plane.ClosestPointOnPlane(pin.transform.position);

            transform.rotation = Quaternion.LookRotation(mappedTargetPosition - transform.position, transform.up);
        }
    }
}
