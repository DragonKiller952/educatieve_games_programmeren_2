using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Planet;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Planet.transform);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
