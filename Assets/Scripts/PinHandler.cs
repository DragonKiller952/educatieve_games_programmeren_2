using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PinHandler : MonoBehaviour
{
    public GameObject planet;
    public LevelHandler gameHandler;
    public new Camera camera;
    public GameObject[] pins1;
    public GameObject[] pins;
    public float dist;
    public GameObject playerObj;
    public PlayerMovement player;
    public PlanetMovement planetMov;
    public bool canSelect = true;

    private Dictionary<string, List<string>> connections;


// Start is called before the first frame update
void Start()
    {
        // Define the pin connections
        DefineConnections();

        // Rotate all pins so they sit upright on the planet surface
        foreach (GameObject pin in pins)
        {
            pin.transform.LookAt(planet.transform);
            pin.transform.Rotate(-90, 0, 0, Space.Self);

            Vector3 dir = (planet.transform.position - pin.transform.position).normalized;
            pin.transform.position = planet.transform.position - dir * dist;
        }
    }

    void Update()
    {
        // If the player is allowed to select a pin, get the selection using
        // raycast and make the player walk towards it
        if (canSelect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Target>() != null && gameHandler.pins.Contains(hitInfo.collider.gameObject))
                    {
                        WalkPath(Pathfinding(hitInfo.collider.gameObject.name));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a dictionary defining all connections the pins have
    /// </summary>
    private void DefineConnections()
    {
        connections = new Dictionary<string, List<string>> {
        {"Netherlands", new List<string>(){"Germany", "Belgium", "UK"}},
        {"Germany", new List<string>(){"Netherlands", "France", "Denmark", "Poland", "CzechRepublic", "Belgium", "Luxembourg", "Switzerland", "Austria"}},
        {"France", new List<string>(){"Germany", "Belgium", "Luxembourg", "UK", "Spain", "Switzerland", "Italy"}},
        {"Denmark", new List<string>(){"Germany", "Sweden", "Norway"}},
        {"Sweden", new List<string>(){"Denmark", "Norway", "Finland"}},
        {"Norway", new List<string>(){"Denmark", "Sweden"}},
        {"Finland", new List<string>(){"Sweden", "Estonia"}},
        {"Estonia", new List<string>(){"Finland", "Latvia"}},
        {"Latvia", new List<string>(){"Estonia", "Lithuania", "Belarus"}},
        {"Lithuania", new List<string>(){"Latvia", "Belarus", "Poland"}},
        {"Belarus", new List<string>(){"Latvia", "Lithuania", "Poland", "Ukraine"}},
        {"Poland", new List<string>(){"Germany", "Lithuania", "Belarus", "Ukraine", "CzechRepublic", "Slovakia"}},
        {"Ukraine", new List<string>(){"Belarus", "Poland", "Hungary", "Slovakia", "Moldova", "Romania"}},
        {"CzechRepublic", new List<string>(){"Germany", "Poland", "Austria", "Slovakia"}},
        {"Belgium", new List<string>(){"Netherlands", "Germany", "France", "Luxembourg", "UK"}},
        {"Luxembourg", new List<string>(){"Germany", "France", "Belgium"}},
        {"UK", new List<string>(){"Netherlands", "France", "Belgium", "Ireland"}},
        {"Ireland", new List<string>(){"UK"}},
        {"Spain", new List<string>(){"France", "Portugal"}},
        {"Portugal", new List<string>(){"Spain"}},
        {"Switzerland", new List<string>(){"Germany", "France", "Italy", "Austria"}},
        {"Italy", new List<string>(){"France", "Switzerland", "Austria", "Slovenia"}},
        {"Austria", new List<string>(){"Germany", "CzechRepublic", "Switzerland", "Italy", "Slovenia", "Hungary", "Slovakia"}},
        {"Slovenia", new List<string>(){"Italy", "Austria", "Hungary", "Croatia"}},
        {"Hungary", new List<string>(){"Ukraine", "Austria", "Slovenia", "Slovakia", "Croatia", "Serbia", "Romania"}},
        {"Slovakia", new List<string>(){"Poland", "Ukraine", "CzechRepublic", "Austria", "Hungary"}},
        {"Croatia", new List<string>(){"Slovenia", "Hungary", "Bosnia", "Serbia"}},
        {"Bosnia", new List<string>(){"Croatia", "Serbia"}},
        {"Serbia", new List<string>(){"Hungary", "Croatia", "Bosnia", "Romania", "Albania", "Macedonia", "Bulgaria"}},
        {"Moldova", new List<string>(){"Ukraine", "Romania"}},
        {"Romania", new List<string>(){"Ukraine", "Hungary", "Serbia", "Moldova", "Bulgaria"}},
        {"Albania", new List<string>(){"Serbia", "Macedonia", "Greece"}},
        {"Macedonia", new List<string>(){"Serbia", "Albania", "Bulgaria", "Greece"}},
        {"Bulgaria", new List<string>(){"Serbia", "Romania", "Macedonia", "Greece"}},
        {"Greece", new List<string>(){"Albania", "Macedonia", "Bulgaria"}}
        };
    }

    /// <summary>
    /// Returns the shortest path to the given destination from the players location
    /// using Dijkstra's algorithm
    /// </summary>
    public GameObject[] Pathfinding(string destination)
    {
        var closestDist = new Dictionary<string, float>();
        var closestConn = new Dictionary<string, string>();

        string current = player.location;

        closestDist.Add(current, 0);
        closestConn.Add(current, "");

        List<string> toCheck = new List<string>();

        // Add the initial locations to check
        foreach (string location in connections[current])
        {
            toCheck.Add(location);
        }

        // While the destination has not been found, try and look for it by
        // looking at the found pins neighbors
        while (!closestConn.ContainsKey(destination))
        {
            List<string> nextCheck = new List<string>();
            foreach (string checkLocation in toCheck)
            {
                float minDistance = 1000000f;
                string nextLocation = "";
                float curDistance;

                // Report all new pins found as neighbors and check which of
                // the known neighbors is the closest to the starting point,
                // after which you document the closest
                foreach (string location in connections[checkLocation])
                {
                    if (!closestDist.ContainsKey(location))
                    {
                        if (!toCheck.Contains(location) && !nextCheck.Contains(location))
                        {
                            nextCheck.Add(location);
                        }
                    }
                    else
                    {
                        curDistance = Vector3.Distance(GameObject.Find(checkLocation).transform.position, GameObject.Find(location).transform.position);
                        curDistance += closestDist[location];
                        if (curDistance < minDistance)
                        {
                            minDistance = curDistance;
                            nextLocation = location;
                        }
                    }
                }
                closestDist.Add(checkLocation, minDistance);
                closestConn.Add(checkLocation, nextLocation);
            }
            toCheck = nextCheck;
        }

        // Turn all the names of the pins in the path into the
        // gameobjects they represent
        List<GameObject> path = new List<GameObject>();
        string pathPoint = destination;
        GameObject curObj = GameObject.Find(current);

        while (pathPoint != current)
        {
            path.Add(GameObject.Find(pathPoint));
            pathPoint = closestConn[pathPoint];

        }

        // Reverse the path so it starts from the current location of the player
        path.Reverse();

        return path.ToArray();
    }

    /// <summary>
    /// Walks the path between the given gameobjects and show the questions
    /// </summary>
    public void WalkPath(GameObject[] path)
    {
        StartCoroutine(moveYaDaftBasterd(path));
    }

    IEnumerator moveYaDaftBasterd(GameObject[] path)
    {
        // Lock player inputs and unlock model movement
        planetMov.canMove = false;
        canSelect = false;
        player.standby = false;

        // Make the model walk towards all pins in the path until it reaches
        // the destination
        foreach (GameObject pin in path)
        {
            planetMov.canMove = false;
            player.pin = pin;
            player.location = pin.name;
            yield return new WaitUntil(() => player.arived == true && Vector3.Distance(player.player.transform.position, player.pin.transform.position) <= 0.4);
        }

        // Lock model movement
        player.standby = true;

        // Open the question screen for the destination pin
        gameHandler.OpenLevel(player.location, playerObj);

    }
}
