using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHandler : MonoBehaviour
{
    public GameObject planet;
    public GameObject[] pins1;
    public GameObject[] pins;
    public float dist;
    public NewPlayerMovement player;
    private PlanetMovement planetMov;

    // Start is called before the first frame update
    void Start()
    {
        planetMov = planet.transform.parent.GetComponent<PlanetMovement>();
        foreach (GameObject pin in pins)
        {
            pin.transform.LookAt(planet.transform);
            pin.transform.Rotate(-90, 0, 0, Space.Self);

            Vector3 dir = (planet.transform.position - pin.transform.position).normalized;
            pin.transform.position = planet.transform.position - dir * dist;
            //print(Vector3.Distance(pin.transform.position, planet.transform.position));
        }

        StartCoroutine(moveYaDaftBasterd());
    }

    IEnumerator moveYaDaftBasterd()
    {
        planetMov.canMove = false;
        foreach (GameObject pin in pins1)
        {
            //print((pin.transform.name, planetMov.canMove));
            planetMov.canMove = false;
            player.pin = pin;
            print("guess ill wait");
            yield return new WaitUntil(() => player.arived == true && Vector3.Distance(player.player.transform.position, player.pin.transform.position) <= 0.2);
        }
        planetMov.canMove = true;

    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
