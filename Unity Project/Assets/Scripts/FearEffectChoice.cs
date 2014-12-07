using UnityEngine;
using System.Collections;

public class FearEffectChoice : MonoBehaviour {

	public EffectName SpecificFearEffect;
	private float cooldown;
	private float fearAmount;
	private 

	// Use this for initialization
	void Start () {
//		switch (SpecificFearEffect) {
//		case EffectName.COLD:
//
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public enum EffectName{
		COLD,
		LIGHTS,
		DEVICE,
		POSSESSION
	}
}
