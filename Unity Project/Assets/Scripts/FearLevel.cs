using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FearLevel : MonoBehaviour {

    public static Dictionary<string,bool> fearPowers = new Dictionary<string, bool>();


    public static bool IsActive(string effectName){
        if (fearPowers [effectName])
            return true;
        else
            return false;
    }

	// Use this for initialization
	void Start () {
        fearPowers.Add("cold", true);
        fearPowers.Add("device", false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
