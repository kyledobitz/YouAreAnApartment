using UnityEngine;
using System.Collections;

public class ScaredyCat : MonoBehaviour
{

    public double fearDecrementAmount = 1.0;
    static double _totalFear = 0.0;
    double _fear = 0.0;
    public double fearFactor = 1.0;

    public static double totalFear { get { return totalFear; } }

    public double fear{ get { return _fear; } }

    public void beScared(double scariness)
    {
        double fright = scariness * fearFactor;
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
