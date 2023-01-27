using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material color;

    private void OnMouseEnter()
    {
        print("naja hij zit er echt in");
        var renderer = GetComponent<Renderer>();
        renderer.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        print("nu niet meer heh");
        var renderer = GetComponent<Renderer>();
        renderer.material.color = color.color;
    }
}
