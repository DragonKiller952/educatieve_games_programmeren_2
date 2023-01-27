using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PinHandler : MonoBehaviour
{
    public GameObject planet;
    public new Camera camera;
    public GameObject[] pins1;
    public GameObject[] pins;
    public float dist;
    public NewPlayerMovement player;
    private PlanetMovement planetMov;
    private bool canSelect = true;

    private Dictionary<string, List<string>> connections;


// Start is called before the first frame update
void Start()
    {
        DefineConnections();

        planetMov = planet.transform.parent.GetComponent<PlanetMovement>();
        foreach (GameObject pin in pins)
        {
            pin.transform.LookAt(planet.transform);
            pin.transform.Rotate(-90, 0, 0, Space.Self);

            Vector3 dir = (planet.transform.position - pin.transform.position).normalized;
            pin.transform.position = planet.transform.position - dir * dist;
        }
        //string path = "Assets/Resources/test.txt";
        //StreamWriter writer = new StreamWriter(path, true);

        //foreach (GameObject pin in pins)
        //{
        //    foreach(GameObject pin1 in pins)
        //    {
        //        if (Vector3.Distance(pin.transform.position, pin1.transform.position) < 2)
        //        {
        //            writer.WriteLine(pin.name + "->" + pin1.name + ": " + Vector3.Distance(pin.transform.position, pin1.transform.position));
        //            print(pin.name + "->" + pin1.name + ": " + Vector3.Distance(pin.transform.position, pin1.transform.position));
        //        }
        //    }
        //}
        //writer.Close();

        //StartCoroutine(moveYaDaftBasterd(pins));
        //GameObject.Find("Netherlands").SetActive(false);
    }

    void Update()
    {
        if (canSelect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Target>() != null)
                    {
                        print(hitInfo.collider.gameObject.name);
                        WalkPath(Pathfinding(hitInfo.collider.gameObject.name));
                    }
                }
            }
        }
    }

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

    private GameObject[] Pathfinding(string destination)
    {
        var closestDist = new Dictionary<string, float>();
        var closestConn = new Dictionary<string, string>();

        string current = player.location;

        closestDist.Add(current, 0);
        closestConn.Add(current, "");

        List<string> toCheck = new List<string>();

        foreach (string location in connections[current])
        {
            toCheck.Add(location);
        }

        while (!closestConn.ContainsKey(destination))
        {
            List<string> nextCheck = new List<string>();
            foreach (string checkLocation in toCheck)
            {
                float minDistance = 1000000f;
                string nextLocation = "";
                float curDistance;

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
                Console.WriteLine(string.Join(", ", nextCheck));
                print(("finished", checkLocation, minDistance, nextLocation));
                closestDist.Add(checkLocation, minDistance);
                closestConn.Add(checkLocation, nextLocation);
            }
            toCheck = nextCheck;
        }
        print("found the destination");

        List<GameObject> path = new List<GameObject>();
        string pathPoint = destination;
        GameObject curObj = GameObject.Find(current);

        while (pathPoint != current)
        {
            print(pathPoint);
            path.Add(GameObject.Find(pathPoint));
            pathPoint = closestConn[pathPoint];

        }

        path.Reverse();

        return path.ToArray();
    }


    public void WalkPath(GameObject[] path)
    {
        StartCoroutine(moveYaDaftBasterd(path));
    }

    IEnumerator moveYaDaftBasterd(GameObject[] path)
    {
        planetMov.canMove = false;
        canSelect = false;
        player.standby = false;
        foreach (GameObject pin in path)
        {
            planetMov.canMove = false;
            player.pin = pin;
            print("guess ill wait");
            player.location = pin.name;
            yield return new WaitUntil(() => player.arived == true && Vector3.Distance(player.player.transform.position, player.pin.transform.position) <= 0.2);
        }
        planetMov.canMove = true;
        canSelect = true;
        player.standby = true;

    }
}
