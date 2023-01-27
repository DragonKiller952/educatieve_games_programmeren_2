using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class NewPlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float dist;
    public float speed;
    public GameObject pin;
    public string location = "Netherlands";
    private Vector3 moveDirection;
    public GameObject planet;
    public bool arived;
    public bool standby = true;
    private Quaternion rot;
    private Vector3 planetRotationDefault;
    private Vector3 parentPlanetRotationDefault;


    void Start()
    {
        player.transform.LookAt(transform);
        player.transform.Rotate(-90, 0, 0, Space.Self);
        Vector3 dir = (transform.position - player.transform.position).normalized;
        player.transform.position = transform.position - dir * dist;
        print(Vector3.Distance(player.transform.position, transform.position));
        planetRotationDefault = planet.transform.localEulerAngles;
        parentPlanetRotationDefault = planet.transform.parent.localEulerAngles;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = (pin.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (!standby)
        {
            arived = false;

            if (Vector3.Distance(player.transform.position, pin.transform.position) > 0.2)
            {
                arived = false;

                var plane = new Plane(player.transform.up, player.transform.position);
                var mappedTargetPosition = plane.ClosestPointOnPlane(pin.transform.position);

                player.transform.rotation = Quaternion.LookRotation(mappedTargetPosition - player.transform.position, player.transform.up);

                Vector3 target = pin.transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, target, speed * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

                planet.transform.localEulerAngles = new Vector3(planet.transform.localEulerAngles.x, (planetRotationDefault.y - transform.localEulerAngles.y) - 5f, planet.transform.localEulerAngles.z);

                Quaternion tempRotation = planet.transform.parent.rotation;
                planet.transform.parent.localEulerAngles = new Vector3(planet.transform.parent.localEulerAngles.x, planet.transform.parent.localEulerAngles.y, (parentPlanetRotationDefault.z - checkEulerAngle(transform.localEulerAngles.x)) + 15f);

                if (planet.transform.parent.rotation.z > 0.35f)
                {
                    planet.transform.parent.rotation = tempRotation;
                }
                if (-0.20f > planet.transform.parent.rotation.z)
                {
                    planet.transform.parent.rotation = tempRotation;
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
