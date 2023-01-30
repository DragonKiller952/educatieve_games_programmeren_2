using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
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
        // Define the animator
        animator = player.GetComponent<Animator>();

        // Define the parent planet for Z rotations
        planetX = planetY.transform.parent.gameObject;

        // Rotate the model to stand level on the planet surface
        //player.transform.LookAt(transform);
        player.transform.Rotate(90, 0, 0, Space.Self);

        // Place the player model given distance from the center of the planet
        Vector3 dir = (transform.position - player.transform.position).normalized;
        player.transform.position = transform.position - dir * dist;

        // Save the starting rotations of the planet for script induced rotations
        planetYRotationDefault = planetY.transform.localEulerAngles;
        planetXRotationDefault = planetX.transform.localEulerAngles;
        
    }

    void Update()
    {
        // If playermodel is not on standby, update the animation
        if (!standby)
        {
            animator.SetBool("Idle", arived);
        }
    }

    void FixedUpdate()
    {
        // If playermodel is not on standby
        if (!standby)
        {
            // If the playermodel has not reached the given pin
            if (Vector3.Distance(player.transform.position, pin.transform.position) > 0.4)
            {
                arived = false;

                // Get the rotation towards the given pin and rotate the playermodel
                var plane = new Plane(player.transform.up, player.transform.position);
                var mappedTargetPosition = plane.ClosestPointOnPlane(pin.transform.position);

                player.transform.rotation = Quaternion.LookRotation(mappedTargetPosition - player.transform.position, player.transform.up);

                // Rotate the player over the surface of the planet
                Vector3 target = pin.transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, target, speed * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

                // Rotate the planet in the opposite direction of the player movement,
                // so the player stays in camera view.

                // Also take the rotation limits into account during this action
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
                // If the player has reached the pin, mark it as such
                location = pin.name;
                arived = true;
            }
        }
    }

    /// <summary>
    /// Manually check the Euler angle, since negatives conflict with the limits
    /// </summary>
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
