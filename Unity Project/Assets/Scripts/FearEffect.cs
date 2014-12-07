using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FearEffect : MonoBehaviour {

    public float effectRadius;
    public float fearAmount;
    public Animation scarimation; //excellent name from Devon
    public string fearEffect;

    void OnMouseDown(){
        Debug.Log("I GOT PRESSED WOOO");
        if (FearLevel.IsActive(fearEffect))
        {
            Debug.Log("fear effect is active");
            //PLAY ANIMATION. COSMO MAKE THIS WORK LOL
            Collider[] nearbyResidents = Physics.OverlapSphere(gameObject.transform.position, effectRadius);
            Debug.Log("nearbyResidents length is: " + nearbyResidents.Length);
            foreach (Collider person in nearbyResidents)
            {
                if (person.gameObject.tag == "Resident")
                {
                    Debug.Log("person is a resident");
                    person.gameObject.GetComponent<ScaredyCat>().beScared(fearAmount);
                }
            }
        }

    }


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
