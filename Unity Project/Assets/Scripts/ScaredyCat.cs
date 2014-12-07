using UnityEngine;
using System.Collections;

public class ScaredyCat : MonoBehaviour
{
    public float fearDecrementAmount = 1f;
    public float _fear = 0f;
    public float fearFactor = 1f;
	private static float _totalFear = 0f;

    public static float totalFear { get { return _totalFear; } }

    public float fear{ get { return _fear; } }

    public void beScared(float scariness)
    {
        float fright = scariness * fearFactor;
        _fear += fright;
        _totalFear += fright;
        audio.Play();
        Debug.Log(gameObject.name + " has fear of " + _fear);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_fear > 0)
            _fear -= fearDecrementAmount * Time.deltaTime;
    }
}
