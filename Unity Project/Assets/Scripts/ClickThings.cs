using UnityEngine;
using System.Collections;

public class ClickThings : MonoBehaviour {

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
            //Debug.Log(hit.collider.gameObject.name);
            FearEffect fear = hit.collider.gameObject.GetComponent<FearEffect>();
            if (fear)
            {
                fear.Scare();
            }
        }
	}
    void Update()
    {
        if (Input.GetMouseButton(0))
            Click();
    }
}
