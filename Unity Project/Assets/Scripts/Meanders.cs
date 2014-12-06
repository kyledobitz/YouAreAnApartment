using UnityEngine;
using System.Collections;

public class Meanders : MonoBehaviour {

    public GameObject[] doorways;
    public BoxCollider currentRoom;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;
    public float standVsMeanderPercent = 0.5f;
    public float minDelayTime = 2f;
    public float maxDelayTime = 5f;

    State currentState = State.STANDING;
    float nextDecisionTime;
    Vector3 meanderDestination;

	// Use this for initialization
	void Start () {
		nextDecisionTime = Time.time + Random.Range (minDelayTime, maxDelayTime);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate (){
        switch (currentState)
        {
            case State.STANDING:
                if(Time.time > nextDecisionTime)
                    currentState = DecideNextState();
                break;
            case State.MEANDERING:
                if(Time.time > nextDecisionTime)
                    currentState = DecideNextState();
                break;
            case State.SWITCHING_ROOMS:
                break;
        }
    }
   

    State DecideNextState(){
        if (Random.value < standVsMeanderPercent)
            return State.STANDING;
        else
            return State.MEANDERING;
		nextDecisionTime = Time.time + Random.Range (minDelayTime, maxDelayTime);
    }

    void Meander(){

    }

    public enum State{
        MEANDERING,
        STANDING,
        SWITCHING_ROOMS
    }
}
