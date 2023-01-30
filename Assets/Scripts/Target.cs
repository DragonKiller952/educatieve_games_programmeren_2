using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool targetable;

    void Start()
    {
        // Set the pin as targetable for OnMouseEnter
        targetable = true;
    }

    private void OnMouseEnter()
    {
        // If it is allowed to be selected, change the color
        if (targetable)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    private void OnMouseExit()
    {
        // If it is allowed to be selected, change the color back
        if (targetable)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
