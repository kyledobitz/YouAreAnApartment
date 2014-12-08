using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Meanders : MonoBehaviour {

    //public Doorway[] doorways;
	public Collider currentRoom;
	public float frontDoorMagnetism = 10f;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;
    public float standVsMeanderPercent = 0.5f;
    public float minDelayTime = 2f;
    public float maxDelayTime = 5f;
	public float doorMagnetism = 3f;
	public GameObject[] frontDoorToStairsPath;

	int frontDoorToStairsPathIndex = 0;
	bool evacuating = false;
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
				velocity = Vector3.zero;
                break;
            case State.MEANDERING:
				if(evacuating == true && IsAtFrontDoor()){
					destination = frontDoorToStairsPath[0].transform.position;
					state = State.MOVING_OUT;
				}
                if(Time.time > nextDecisionTime || IsAtDestination()){
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
			case State.MOVING_OUT:
				if(IsAtStairs()){
					Destroy(gameObject);
				}
				else if(IsAtDestination()){
					destination = frontDoorToStairsPath[frontDoorToStairsPathIndex].transform.position;
					frontDoorToStairsPathIndex++;
				}
				break;
        }
		animator.SetFloat ("Speed", velocity.magnitude);

		if (state != State.STANDING) {
			Move();
		}
	}

	public void BeginEvacuating(){
		evacuating = true;
		state = State.MEANDERING;
		speed = maxSpeed;
		minDelayTime = minDelayTime / 2f;
		maxDelayTime = maxDelayTime / 2f;
		standVsMeanderPercent = 0f;
		frontDoorToStairsPathIndex = 0;
	}
	
	bool IsAtStairs(){
		var stairs = frontDoorToStairsPath [frontDoorToStairsPath.Length - 1];
		return (transform.position - stairs.transform.position).magnitude < 0.2f;
	}

    bool IsAtDestination(){
        return (transform.position - destination).magnitude < 0.2f;
    }

	bool IsAtFrontDoor(){
		var frontDoor = frontDoorToStairsPath [0];
		return (transform.position - frontDoor.transform.position).magnitude < frontDoorMagnetism;
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
        GOING_TO_DOOR,
		MOVING_IN,
		MOVING_OUT,
    }
}
