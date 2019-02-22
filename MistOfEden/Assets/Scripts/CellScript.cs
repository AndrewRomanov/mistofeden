using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
    }
}