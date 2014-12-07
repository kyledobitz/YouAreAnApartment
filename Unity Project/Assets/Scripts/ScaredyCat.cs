﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaredyCat : MonoBehaviour
{
    public GameObject fearMeter;
    Slider fearSlider;
    

    public float maxFear = 120f;
    public float fearDecrementAmount = 1f;
    public float _fear = 0f;
    public float fearFactor = 1f;
	private static float _totalFear = 0f;

    Camera gameCamera;

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
        fearMeter = (GameObject) Instantiate(fearMeter);
        fearSlider = fearMeter.GetComponentInChildren<Slider>();
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fearSlider.minValue = 0;
        fearSlider.maxValue = maxFear;

    }

    // Update is called once per frame
    void Update()
    {
        fearSlider.transform.position = gameCamera.WorldToScreenPoint(gameObject.transform.position);

        fearSlider.value = _fear;
        if (_fear > 0)
            _fear -= fearDecrementAmount * Time.fixedDeltaTime;
    }
}
