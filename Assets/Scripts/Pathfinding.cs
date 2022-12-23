using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject pin;
    // Start is called before the first frame update
    void Start()
    {
        print(coordCalc(pin.transform.position));
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private static Vector2 coordCalc(Vector3 pos)
    {
        float radius = Mathf.Sqrt(Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y, 2) + Mathf.Pow(pos.z, 2));
        float incl = Mathf.Acos(pos.z / radius);
        float azim = Mathf.Sign(pos.y) * Mathf.Acos(pos.x / Mathf.Sqrt(Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y, 2)));
        print((radius, incl, azim));
        Vector2 coord = new Vector2(incl, azim);

        return coord;
    }
}
