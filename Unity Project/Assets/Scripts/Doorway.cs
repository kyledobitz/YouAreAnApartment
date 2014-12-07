using UnityEngine;
using System.Collections;

public class Doorway : MonoBehaviour {

    public Collider firstRoom;
    public Collider secondRoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Collider GetNextRoom(Collider previousRoom){
        if (previousRoom.GetInstanceID() == firstRoom.GetInstanceID())
            return secondRoom;
        else
            return firstRoom;
    }
}
