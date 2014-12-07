using UnityEngine;
using System.Collections;

public class TransparencyAdjustment : MonoBehaviour {

    [Range(0,1)]
    public float alphaAdjust = 1.0f;

    void Start () {
        
    }

    void Update() {
        // guy in a forum said something like this to get children of children
        // gameObject.GetComponentsInChildren(Transform);
        foreach (Transform child in gameObject.transform) {

        Color color = child.renderer.material.color;
        color.a = alphaAdjust;
        child.renderer.material.color = color;
        }
}
}