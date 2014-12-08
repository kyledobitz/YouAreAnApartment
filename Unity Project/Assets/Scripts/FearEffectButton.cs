using UnityEngine;
using System.Collections;


[RequireComponent(typeof(ScaryObject))]
[RequireComponent(typeof(Collider))]
public class FearEffectButton : MonoBehaviour {

	ScaryObject relevantFearEffect;
	public Vector3 buttonPosition;
	public Vector3 hidingPosition;
	private bool inPlace = false;

	void OnMouseDown()
	{
		if (relevantFearEffect.thisEffect.canBeUsed && !relevantFearEffect.thisEffect.isActive)
		{
			relevantFearEffect.thisEffect.isActive = true;
			ClickThings.awaitingClick = true;
			Debug.Log("BUTTON CLICKED!");
		}

	}

	// Use this for initialization
	void Start () {
		relevantFearEffect = gameObject.GetComponent<ScaryObject> ();
		gameObject.transform.position = hidingPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (relevantFearEffect.thisEffect.canBeUsed && !inPlace)
		{
			Debug.Log("BUTTON REVEALED!");
			gameObject.transform.position = buttonPosition;
			inPlace = true;
		}
	}
}
