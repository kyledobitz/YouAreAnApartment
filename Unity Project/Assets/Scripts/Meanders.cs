using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Meanders : MonoBehaviour {

    //public Doorway[] doorways;
    public Collider currentRoom;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;
    public float standVsMeanderPercent = 0.5f;
    public float minDelayTime = 2f;
    public float maxDelayTime = 5f;
    public float doorMagnetism = 5f;

    float nextDecisionTime;
    State state = State.STANDING;
    Vector3 destination;
    float speed;
    Vector3 velocity;
    Vector3 heading;

	// Use this for initialization
	void Start () {
        nextDecisionTime = Time.time;
        speed = minSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.MEANDERING:
                Move();
                break;
            case State.STANDING:
                break;
            case State.GOING_TO_DOOR:
                Move();
                break;
        }
	}

    void FixedUpdate (){
        switch (state)
        {
            case State.STANDING:
                if(Time.time > nextDecisionTime){
                    nextDecisionTime = getNextDecisionTime();
                    state = DecideNextState();
                }
                break;
            case State.MEANDERING:
                if(IsAtDestination()){
                    state = State.STANDING;
                }
                if(Time.time > nextDecisionTime){
                    nextDecisionTime = getNextDecisionTime();
                    state = DecideNextState();
                }
                break;
            case State.GOING_TO_DOOR:
                if(IsAtDestination()){
                    nextDecisionTime = getNextDecisionTime();
                    Collider nextRoom = GetNearbyDoor().GetNextRoom(currentRoom);
                    currentRoom = nextRoom;
                    ChooseMeanderDestination();
                    state = State.MEANDERING;
                }
                break;
        }
    }

    bool IsAtDestination(){
        return (transform.position - destination).magnitude < 0.01f;
    }
   
    float getNextDecisionTime(){
        return Time.time + Random.Range (minDelayTime, maxDelayTime);
    }

    Doorway GetNearbyDoor(){
        //var doorways = Resources.FindObjectsOfTypeAll(typeof(Doorway));//
        var doorwayObjects = GameObject.FindGameObjectsWithTag("Doorway");
        foreach (GameObject doorwayObject in doorwayObjects)
        {
            Doorway doorway = doorwayObject.GetComponentInChildren<Doorway>();
            if((transform.position - doorway.transform.position).magnitude < doorMagnetism)
                return doorway;
        }
        return null;
    }

    State DecideNextState(){
        Doorway nearbyDoor = GetNearbyDoor();
        if(nearbyDoor != null){
            destination = nearbyDoor.transform.position;
            return State.GOING_TO_DOOR;
        }
        if (Random.value < standVsMeanderPercent)
        {
            audio.Play();
            return State.STANDING;
        } else 
        {
            ChooseMeanderDestination();
            return State.MEANDERING;
        }
    }

    void ChooseMeanderDestination(){
        var minX = currentRoom.bounds.min.x;
        var minZ = currentRoom.bounds.min.z;
        var maxX = currentRoom.bounds.max.x;
        var maxZ = currentRoom.bounds.max.z;
        destination = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        //Debug.Log("next meander destination:" + destination.ToString());
    }

    void Move(){
        if((transform.position - destination).magnitude < 0.01f) 
            return;
        heading = (destination - transform.position).normalized;
        transform.forward = heading;
        velocity = heading * speed;
        transform.position += Time.deltaTime*velocity;
    }

    public enum State{
        MEANDERING,
        STANDING,
        GOING_TO_DOOR
    }
}
