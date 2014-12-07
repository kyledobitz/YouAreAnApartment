using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FearEffect : MonoBehaviour {

    public Collider effectArea;
    public float fearAmount;
    public Animation scarimation; //excellent name from Devon
    public string fearEffect;

    List<GameObject> residents = new List<GameObject>();

    void OnMouseDown(){
        Debug.Log("I GOT PRESSED WOOO");
        if (FearLevel.IsActive(fearEffect))
        {
            //PLAY ANIMATION. COSMO MAKE THIS WORK LOL
            foreach (GameObject person in residents)
            {
                if (person.tag == "resident")
                    person.GetComponent<ScaredyCat>().beScared(fearAmount);
            }
        }

    }

    void OnTriggerEnter(Collider other){
        residents.Add(other.gameObject);
        Debug.Log("SOMEBODY IS HERE WOOO");
    }


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
