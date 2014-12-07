using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Meanders : MonoBehaviour {

    public GameObject[] doorways;
    public BoxCollider currentRoom;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;
    public float standVsMeanderPercent = 0.5f;
    public float minDelayTime = 2f;
    public float maxDelayTime = 5f;

    float nextDecisionTime;
    State state = State.STANDING;
    Vector3 destination;
    float speed;
    Vector3 velocity;
    Vector3 heading;

	// Use this for initialization
	void Start () {
        nextDecisionTime = getNextDecisionTime();
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
            case State.SWITCHING_ROOMS:
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
                if((transform.position - destination).magnitude < 0.01f){
                    state = State.STANDING;
                }
                if(Time.time > nextDecisionTime){
                    nextDecisionTime = getNextDecisionTime();
                    Debug.Log("new decision time: " + nextDecisionTime);
                    state = DecideNextState();
                }
                break;
            case State.SWITCHING_ROOMS:
                break;
        }
    }
   
    float getNextDecisionTime(){
        return Time.time + Random.Range (minDelayTime, maxDelayTime);
    }

    State DecideNextState(){
        if (Random.value < standVsMeanderPercent)
        {
            audio.Play();
            return State.STANDING;
        }
        else 
            ChooseMeanderDestination();
            return State.MEANDERING;
    }

    void ChooseMeanderDestination(){
        var minX = currentRoom.bounds.min.x;
        var minZ = currentRoom.bounds.min.z;
        var maxX = currentRoom.bounds.max.x;
        var maxZ = currentRoom.bounds.max.z;
        destination = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        Debug.Log("next meander destination:" + destination.ToString());
    }

    void Move(){
        heading = (destination - transform.position).normalized;
        transform.forward = heading;
        velocity = heading * speed;
        transform.position += Time.deltaTime*velocity;
    }

    public enum State{
        MEANDERING,
        STANDING,
        SWITCHING_ROOMS
    }
}
