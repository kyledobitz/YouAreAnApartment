using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Apartment : MonoBehaviour {

	public GameObject apartment;

	public GameObject[] frontDoorToStairsPath;
	public Collider[] rooms;
	public GameObject[] doorways;

	public int maxPeopleNumber;
	public int minAdultNumber;
	public int maxAdultNumber;
	public GameObject[] adultPrefabs;
	public int minChildNumber;
	public int maxChildNumber;
	public GameObject[] childPrefabs;
	public float minVacancyTime;
	public float maxVacancyTime;
	public float minComingUpStairsTime;
	public float maxComingUpStairsTime;

	float nextComingUpStairsTime;
	float endVacancyTime;
    public List<GameObject> residents;
	State state;

	List<GameObject> CreateResidents(){
		int numberOfAdults = Random.Range (minAdultNumber, maxAdultNumber);
		int numberOfChildren = Random.Range (minChildNumber, maxChildNumber);
		if (numberOfAdults + numberOfChildren > maxPeopleNumber)
			numberOfChildren = maxPeopleNumber - numberOfAdults;

		List<GameObject> newResidents = new List<GameObject> ();
		for (int i = 0; i < numberOfAdults; i++) {
			var prefabToUse = adultPrefabs[Random.Range(0,adultPrefabs.Length-1)];
			newResidents.Add((GameObject) Instantiate(prefabToUse));
		}
		for (int i = 0; i < numberOfChildren; i++) {
			var prefabToUse = childPrefabs[Random.Range(0,childPrefabs.Length-1)];
			newResidents.Add((GameObject) Instantiate(prefabToUse));
		}
        var family = new GameObject ("Family");
        family.transform.SetParent (apartment.transform);
        foreach (GameObject resident in newResidents)
        {
			resident.transform.SetParent(family.transform);
			Meanders meanderer = (Meanders)resident.GetComponent (typeof(Meanders));
			meanderer.frontDoorToStairsPath = frontDoorToStairsPath;
			meanderer.doorways = doorways;
		}
		return newResidents;
	}

	void PlaceResidentInRoom(GameObject resident){
		Collider randomRoom = rooms [Random.Range (0, rooms.Length - 1)];
		var minX = randomRoom.bounds.min.x;
		var minZ = randomRoom.bounds.min.z;
		var maxX = randomRoom.bounds.max.x;
		var maxZ = randomRoom.bounds.max.z;
		resident.transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
		resident.transform.forward = new Vector3 (Random.value, 0, Random.value).normalized;
		Meanders meanderer = (Meanders)resident.GetComponent (typeof(Meanders));
		meanderer.currentRoom = randomRoom;
	}

	// Use this for initialization
    void Start () {
        residents = CreateResidents ();
        foreach (GameObject resident in residents)
        {
            PlaceResidentInRoom(resident);
        }
	}

	bool IsFearTooHigh(){
		foreach (GameObject resident in residents) {
			ScaredyCat cat = (ScaredyCat) resident.GetComponent (typeof(ScaredyCat));
			if(cat.maxFear - cat.fear < 0.5f) return true;
		}
		return false;
	}

	void BeginEvacuation(){
		Debug.Log ("EVACUATING " + residents.Count);
		foreach (GameObject resident in residents) {
			Meanders meanderer = (Meanders)resident.GetComponent (typeof(Meanders));
			meanderer.BeginEvacuating();
		}
	}

	void BeginMoving(){
		Debug.Log ("MOVING");
		residents = CreateResidents ();
		foreach (GameObject resident in residents) {
			resident.SetActive(false);
		}
		nextComingUpStairsTime = Time.time;
	}

	void SendResidentUpStairs(){
		foreach (GameObject resident in residents) {
			if(resident.activeSelf){ 
				continue;			
			}
			resident.transform.position = frontDoorToStairsPath[frontDoorToStairsPath.Length-1].transform.position;
			Meanders meanderer = (Meanders)resident.GetComponent (typeof(Meanders));
			meanderer.currentRoom = rooms[0];
			meanderer.BeginMovingIn();
			resident.SetActive(true);
			return;
		}
		Debug.Log ("FULL");
		state = State.FULL;
	}

	// Update is called once per frame
	void Update () {
		residents.RemoveAll (item => item == null);
		switch (state) {
		case State.FULL:
			if(IsFearTooHigh()){
				BeginEvacuation();
				state = State.EVACUATING;
			}
			break;
		case State.EVACUATING:
			if(residents.Count == 0){
				Debug.Log("VACANT");
				endVacancyTime = Time.time + Random.Range(minVacancyTime,maxVacancyTime);
				state = State.VACANT;
			}
			break;
		case State.VACANT:
			if(Time.time > endVacancyTime){
				BeginMoving();

				state = State.MOVING;
			}
			break;
		case State.MOVING:
			if(Time.time > nextComingUpStairsTime){
				SendResidentUpStairs();
				nextComingUpStairsTime = Time.time + Random.Range(minComingUpStairsTime, maxComingUpStairsTime);
			}
			break;
		}
	}
	
	public enum State{
		FULL,
		EVACUATING,
		VACANT,
		MOVING
	}
}
