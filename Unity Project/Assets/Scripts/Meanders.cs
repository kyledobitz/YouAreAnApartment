using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Meanders : MonoBehaviour {

    //public Doorway[] doorways;
    public Collider currentRoom;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;
    public float standVsMeanderPercent = 0.5f;
    public float minDelayTime = 2f;
    public float maxDelayTime = 5f;
    public float doorMagnetism = 5f;

	Animator animator;
    float nextDecisionTime;
    State state = State.STANDING;
    Vector3 destination;
    float speed;
    Vector3 velocity;
    Vector3 heading;

	void Awake()
	{
		animator = transform.GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
        nextDecisionTime = Time.time;
        speed = minSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        // Handle state changes
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
					animator.SetBool ("Walk", false);
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
					animator.SetBool ("Walk", true);
                }
                break;
        }

        // Handle Movements
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
    }

    bool IsAtDestination(){
        return (transform.position - destination).magnitude < 0.2f;
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
			animator.SetBool ("Walk", true);
            return State.GOING_TO_DOOR;
        }
        if (Random.value < standVsMeanderPercent)
		{
			animator.SetBool ("Walk", false);
            return State.STANDING;
        } else 
        {
			ChooseMeanderDestination();
			animator.SetBool ("Walk", true);
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
        if((transform.position - destination).magnitude < 0.1f) 
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
