using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(ScaryObject))]
[RequireComponent(typeof(Collider))]
public class FearEffectButton : MonoBehaviour {

	ScaryObject relevantFearEffect;

	private bool inPlace = false;

	private Vector3 smallSize = new Vector3(0.2f,0.2f,0.2f);
	private Vector3 largeSize = new Vector3(0.15f,0.15f,0.15f);


	void OnMouseDown()
	{
		Debug.Log ("relevantFearEffect.thisEffect.isActive = " + relevantFearEffect.thisEffect.isActive);
		if (relevantFearEffect.thisEffect.canBeUsed && !relevantFearEffect.thisEffect.isActive &&  Time.time >= relevantFearEffect.thisEffect.readyAt )
		{
			relevantFearEffect.thisEffect.isActive = true;
			MarkSelected();
			foreach (KeyValuePair<string,ScaryObject.Effect> entry in ScaryObject.fearEffects)
			{
				if (entry.Value != relevantFearEffect.thisEffect)
					entry.Value.isActive = false;
            }
        }
		else if (relevantFearEffect.thisEffect.isActive)
		{
			relevantFearEffect.thisEffect.isActive = false;
			MarkUnselected();
		}

	}

	public void MarkSelected(){
		gameObject.transform.localScale = largeSize;
	}
	public void MarkUnselected(){
		gameObject.transform.localScale = smallSize;
	}

	// Use this for initialization
	void Start () {

		relevantFearEffect = gameObject.GetComponent<ScaryObject> ();
		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (relevantFearEffect.thisEffect.canBeUsed && !inPlace)
		{
			gameObject.renderer.enabled = true;
			inPlace = true;
		}
		if (!relevantFearEffect.thisEffect.isActive)
			MarkUnselected();
		
	}
}
