using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Apartment : MonoBehaviour {

	public GameObject apartment;

	public Collider[] rooms;
	public GameObject[] Doorways;

	public int maxPeopleNumber;
	public int minAdultNumber;
	public int maxAdultNumber;
	public GameObject[] adultPrefabs;
	public int minChildNumber;
	public int maxChildNumber;
	public GameObject[] childPrefabs;

    List<GameObject> residents;
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
		((Meanders) resident.GetComponent (typeof(Meanders))).currentRoom = randomRoom;
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
			if(cat.maxFear - cat.fear < 0.5) return true;
		}
		return false;
	}

	void BeginEvacuation(){
		Debug.Log ("Evacuating");
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.FULL:
			if(IsFearTooHigh())
				BeginEvacuation();
			break;
		}
	}
	
	public enum State{
		FULL,
		EVACUATING,
		VACANT
	}
}
