using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FearLevel : MonoBehaviour {

    public float coldPowerThreshold = 0f;
    public float lightsPowerThreshold = 0f;
    public float devicePowerThreshold = 100f;

    public static float totalFear{
		get {
				float output = 0f;
				foreach (GameObject resident in GameObject.FindGameObjectsWithTag("Resident"))
				{
					output += resident.GetComponent<ScaredyCat> ().fear;
				}
				if (output > 0)
					return output;
				else
					return 0;
			}

	}

    public static Dictionary<string,bool> fearPowers = new Dictionary<string, bool>();


    public static bool IsActive(string effectName){
        if (fearPowers [effectName])
            return true;
        else
            return false;
    }

	public static ScaryObject.Effect cold = new ScaryObject.Effect (10f, 10f,0f);
	public static ScaryObject.Effect lights = new ScaryObject.Effect (10f, 10f,0f);
	public static ScaryObject.Effect device = new ScaryObject.Effect (10f, 10f,300f);
	public static ScaryObject.Effect fling = new ScaryObject.Effect (10f, 10f,500f);
	public static ScaryObject.Effect possession = new ScaryObject.Effect (10f, 10f,1000f);




	// Use this for initialization
	void Start () {
		cold.isActive = true;
		ScaryObject.fearEffects.Add ("cold", cold); 
		ScaryObject.fearEffects.Add ("lights", lights);
		ScaryObject.fearEffects.Add ("device", device);
		ScaryObject.fearEffects.Add ("fling", fling);
		ScaryObject.fearEffects.Add ("possession", possession);
        fearPowers.Add("cold", true);
        fearPowers.Add("device", false);
        fearPowers.Add ("lights", true);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyValuePair<string,ScaryObject.Effect> entry in ScaryObject.fearEffects)
		{
			ScaryObject.Effect temp = entry.Value;
			if (totalFear >= temp.fearRequiredToUnlock)
			{
				temp.canBeUsed = true;
			}
				
		}



	}
    
}
