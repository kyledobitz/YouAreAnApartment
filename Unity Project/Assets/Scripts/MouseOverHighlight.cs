using UnityEngine;
using System.Collections;

public class MouseOverHighlight : MonoBehaviour {

    private Color startcolor;
    void OnMouseEnter()
    {
        startcolor = renderer.material.color;
        renderer.material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        renderer.material.color = startcolor;
    }
}
