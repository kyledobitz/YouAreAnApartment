using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float lifetime = 5.0f;
	private float deathtime;

	// Use this for initialization
	void Awake () {
		deathtime = Time.time + lifetime;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > this.deathtime)
			GameObject.Destroy (gameObject);
	}
}
