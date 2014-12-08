using UnityEngine;
using System.Collections;


[RequireComponent(typeof(ScaryObject))]
[RequireComponent(typeof(Collider))]
public class FearEffectButton : MonoBehaviour {

	ScaryObject relevantFearEffect;
	public Vector3 buttonPosition;
	public Vector3 hidingPosition;
	private bool inPlace = false;

	private Vector3 smallSize = new Vector3(3,3,3);
	private Vector3 largeSize = new Vector3(2,2,2);


	void OnMouseDown()
	{
		Debug.Log ("relevantFearEffect.thisEffect.isActive = " + relevantFearEffect.thisEffect.isActive);
		if (relevantFearEffect.thisEffect.canBeUsed && !relevantFearEffect.thisEffect.isActive &&  Time.time >= relevantFearEffect.thisEffect.readyAt )
		{
			relevantFearEffect.thisEffect.isActive = true;
			MarkSelected();
			Debug.Log("BUTTON CLICKED!");
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
		gameObject.transform.position = hidingPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (relevantFearEffect.thisEffect.canBeUsed && !inPlace)
		{
			gameObject.transform.position = buttonPosition;
			inPlace = true;
		}
		if (!relevantFearEffect.thisEffect.isActive)
			MarkUnselected();
		
	}
}
