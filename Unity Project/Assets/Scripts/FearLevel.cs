using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FearLevel : MonoBehaviour {

    public float coldPowerThreshold = 0;
    public float lightsPowerThreshold = 0;
    public float devicePowerThreshold = 100;

    public float coldPowerCooldown = 10;
    public float lightsPowerCooldown = 10;
    public float devicePowerCooldwon = 10;
    public static float totalFear = 0;

    public static Dictionary<string,bool> fearPowers = new Dictionary<string, bool>();


    public static bool IsActive(string effectName){
        if (fearPowers [effectName])
            return true;
        else
            return false;
    }


    public void CheckForAbilityUnlocks()
    {
        if (totalFear > devicePowerThreshold)
            fearPowers ["device"] = true;
    }




	// Use this for initialization
	void Start () {
        fearPowers.Add("cold", true);
        fearPowers.Add("device", false);
        fearPowers.Add ("lights", true);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
