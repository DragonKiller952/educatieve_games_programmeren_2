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
    private Vector3 moveDirection;
    public GameObject planet;
    public bool arived;
    private Quaternion rot;
    private Vector3 planetRotationDefault;
    private Vector3 parentPlanetRotationDefault;

    // Start is called before the first frame update
    private void Awake()
    {
        print(transform.localEulerAngles);
    }

    void Start()
    {
        print(transform.localEulerAngles);
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
        //print((pin.transform.position, transform.position, pin.transform.position - transform.position, Vector3.Distance(transform.position, pin.transform.position)));
        //moveDirection.y = 0;
    }

    void FixedUpdate()
    {
        arived = false;

        if (Vector3.Distance(player.transform.position, pin.transform.position) > 0.2)
        {
            arived = false;
            //print(("not empty", rot));
            
            //transform.LookAt(new Vector3(pin.transform.position.x, transform.position.y, pin.transform.position.z));

            var plane = new Plane(player.transform.up, player.transform.position);
            var mappedTargetPosition = plane.ClosestPointOnPlane(pin.transform.position);

            player.transform.rotation = Quaternion.LookRotation(mappedTargetPosition - player.transform.position, player.transform.up);


            //rot = Quaternion.LookRotation(pin.transform.localPosition).normalized;

            Vector3 target = pin.transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, target, speed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            ////transform.localRotation = Quaternion.Slerp(transform.localRotation, (Quaternion)rot, speed);
            //transform.Rotate(rot.x, rot.y, rot.z, Space.Self);
            planet.transform.localEulerAngles = new Vector3(planet.transform.localEulerAngles.x, (planetRotationDefault.y - transform.localEulerAngles.y) - 35f, planet.transform.localEulerAngles.z);

            //print((new Vector3(planetY.transform.localEulerAngles.x, (planetRotationDefault.y - transform.localEulerAngles.y) - 35f, planetY.transform.localEulerAngles.z), planetY.transform.localEulerAngles.normalized, transform.localEulerAngles));
            Quaternion tempRotation = planet.transform.parent.rotation;
            planet.transform.parent.localEulerAngles = new Vector3(planet.transform.parent.localEulerAngles.x, planet.transform.parent.localEulerAngles.y, (parentPlanetRotationDefault.z - checkEulerAngle(transform.localEulerAngles.x)) + 15f);
            //print(planetY.transform.parent.rotation.z);
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
            arived = true;
        }
        //print(("player arived:", arived, pin.transform.name));
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
