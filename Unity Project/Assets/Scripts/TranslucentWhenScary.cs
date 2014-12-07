using UnityEngine;
using System.Collections;

public class TranslucentWhenScary : MonoBehaviour {
    public float fearThreshholdToBecomeClear = 0f;
    public Shader opaque;
    public Shader transparent;
	// Use this for initialization
	void Start () {
        opaque = Shader.Find("VertexLit");
        transparent = Shader.Find("Transparent/VertexLit");
	}
	
	// Update is called once per frame
	void Update () {
        if (FearLevel.totalFear >= fearThreshholdToBecomeClear)
            renderer.material.shader = transparent;
        else
            renderer.material.shader = opaque;
	}
}
