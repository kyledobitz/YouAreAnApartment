using UnityEngine;
using System.Collections;

public class ScaredyCat : MonoBehaviour
{
    public float fearDecrementAmount = 1f;
    float _fear = 0f;
    public float fearFactor = 1f;

    public static float totalFear { get { return _totalFear; } }

    public float fear{ get { return _fear; } }

    public void beScared(float scariness)
    {
        float fright = scariness * fearFactor;
        _fear += fright;
        fearFactor += fright;
        if (fearFactor < 0f)
            fearFactor = 0f;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        beScared(-fearDecrementAmount * Time.deltaTime);
    }
}
