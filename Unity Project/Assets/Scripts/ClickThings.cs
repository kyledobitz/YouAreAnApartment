using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickThings : MonoBehaviour {


	public static bool awaitingClick = false;
	// Use this for initialization
	void Start () {
	
	}

    RaycastHit[] hits;
	
	// Update is called once per frame
	void Click () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits)
        {
            ScaryObject fear = hit.collider.gameObject.GetComponent<ScaryObject>();
            if (fear)
            {
                fear.Scare();
            }
        }
	}
    void Update()
    {
        if (Input.GetMouseButton (0)) 
		{
			Click ();
//			if (!awaitingClick)
//				foreach (KeyValuePair<string,ScaryObject.Effect> entry in ScaryObject.fearEffects)
//					{
//						entry.Value.isActive = false;
//					}
//			else
//				awaitingClick = false;
		}
    }
}
