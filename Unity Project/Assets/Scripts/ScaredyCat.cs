using UnityEngine;
using System.Collections;

public class ScaredyCat : MonoBehaviour
{

    public float fearDecrementAmount = 1.0;
    static float _totalFear = 0.0;
    float _fear = 0.0;
    public float fearFactor = 1.0;

    public static float totalFear { get { return totalFear; } }

    public float fear{ get { return _fear; } }

    public void beScared(float scariness)
    {
        float fright = scariness * fearFactor;
        _fear += fright;
        fearFactor += fright;
        if (fearFactor < 0)
            fearFactor = 0;
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
