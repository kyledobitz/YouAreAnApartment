﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FearLevel : MonoBehaviour
{
	public GameObject coldParticles;
	public GameObject deviceParticles;
	public GameObject possessionParticles;

	public float coldPowerThreshold = 0f;
	public float lightsPowerThreshold = 0f;
	public float devicePowerThreshold = 100f;
	public float apartmentUnlockThreshold = 300f;
	public GameObject[] roofs;
	public GameObject[] walls;
	public GameObject[] apartments;
	public static float totalFear;

	public float countDownToQuit = 1000000f;

	float CalculateTotalFear ()
	{
		float sum = 0f;
		foreach (GameObject apartmentObject in apartments) {
			Apartment apartment = (Apartment)apartmentObject.GetComponent (typeof(Apartment));
			apartment.residents.RemoveAll (item => item == null);
			foreach (GameObject residentObject in apartment.residents){
				ScaredyCat cat = (ScaredyCat)residentObject.GetComponent (typeof(ScaredyCat));
				sum += cat.fear;
			}
		}
		return sum;
	}

	public static Dictionary<string,bool> fearPowers = new Dictionary<string, bool> ();

	public static bool IsActive (string effectName)
	{
		if (fearPowers [effectName])
			return true;
		else
			return false;
	}

	public static ScaryObject.Effect cold = new ScaryObject.Effect (1.5f, 20f, -5f);
	public static ScaryObject.Effect lights = new ScaryObject.Effect (0.1f, 50f, 0f);
	public static ScaryObject.Effect device = new ScaryObject.Effect (1.5f, 50f, 300f);
	public static ScaryObject.Effect fling = new ScaryObject.Effect (0.1f, 30f, 500f);
	public static ScaryObject.Effect possession = new ScaryObject.Effect (1f, 1000f, 1200f);

	// Use this for initialization
	void Start ()
	{
		cold.prefab = coldParticles;
		device.prefab = deviceParticles;
		possession.prefab = possessionParticles;

		ScaryObject.fearEffects.Add ("cold", cold); 
		ScaryObject.fearEffects.Add ("lights", lights);
		ScaryObject.fearEffects.Add ("device", device);
		ScaryObject.fearEffects.Add ("fling", fling);
		ScaryObject.fearEffects.Add ("possession", possession);
		fearPowers.Add ("cold", true);
		fearPowers.Add ("device", false);
		fearPowers.Add ("lights", true);
		foreach (GameObject apartment in apartments) {
			apartment.SetActive(false);
		}
		roofs [0].renderer.enabled = false;
		apartments [0].SetActive (true);
	}

	void ChangeLockedApartments ()
	{
		int count = roofs.Length;
		for (int i = 0; i < count; i++) {
			if (totalFear > i * apartmentUnlockThreshold) {
				roofs [i].renderer.enabled = false;
				apartments [i].SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKey)
			DestroyOnClick.destroyCount += 1;
		totalFear = CalculateTotalFear ();
		ChangeLockedApartments ();

		foreach (KeyValuePair<string,ScaryObject.Effect> entry in ScaryObject.fearEffects) {
			ScaryObject.Effect temp = entry.Value;
			if (totalFear >= temp.fearRequiredToUnlock) {
				temp.canBeUsed = true;
			}		
		}

		if (totalFear > 10000) {
						//YOU WIN! Not sure how to do this.
				}
	}
    
}
