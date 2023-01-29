using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float dist;
    public float speed;
    public GameObject pin;
    public string location = "Netherlands";
    private Vector3 moveDirection;
    public GameObject planetY;
    private GameObject planetX;
    public bool arived;
    public bool standby = true;
    private Quaternion rot;
    private Vector3 planetYRotationDefault;
    private Vector3 planetXRotationDefault;


    void Start()
    {
        planetX = planetY.transform.parent.gameObject;

        player.transform.LookAt(transform);
        player.transform.Rotate(-90, 0, 0, Space.Self);
        Vector3 dir = (transform.position - player.transform.position).normalized;
        player.transform.position = transform.position - dir * dist;
        planetYRotationDefault = planetY.transform.localEulerAngles;
        planetXRotationDefault = planetX.transform.localEulerAngles;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!standby)
        {
            moveDirection = (pin.transform.position - transform.position).normalized;
        }
    }

    void FixedUpdate()
    {
        if (!standby)
        {
            if (Vector3.Distance(player.transform.position, pin.transform.position) > 0.2)
            {
                arived = false;

                var plane = new Plane(player.transform.up, player.transform.position);
                var mappedTargetPosition = plane.ClosestPointOnPlane(pin.transform.position);

                player.transform.rotation = Quaternion.LookRotation(mappedTargetPosition - player.transform.position, player.transform.up);

                Vector3 target = pin.transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, target, speed * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

                planetY.transform.localEulerAngles = new Vector3(planetY.transform.localEulerAngles.x, (planetYRotationDefault.y - transform.localEulerAngles.y) - 5f, planetY.transform.localEulerAngles.z);

                Quaternion tempRotation = planetX.transform.rotation;
                planetX.transform.localEulerAngles = new Vector3(planetX.transform.localEulerAngles.x, planetX.transform.localEulerAngles.y, (planetXRotationDefault.z - checkEulerAngle(transform.localEulerAngles.x)) + 15f);

                if (planetX.transform.rotation.z > 0.35f)
                {
                    planetX.transform.rotation = tempRotation;
                }
                if (-0.20f > planetX.transform.rotation.z)
                {
                    planetX.transform.rotation = tempRotation;
                }
            }
            else
            {
                location = pin.name;
                arived = true;
            }
        }
    }

    private float checkEulerAngle(float angle)
    {
        if (angle <= 180)
        {
            return angle;
        }
        else
        {
            return 360 - angle;
        }

    }
}
