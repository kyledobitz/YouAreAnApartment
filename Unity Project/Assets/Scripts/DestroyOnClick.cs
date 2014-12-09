using UnityEngine;
using System.Collections;

public class DestroyOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public static int destroyCount = 0;
	public int destroyNumber;
	
	// Update is called once per frame
	void Update () {

		if (destroyCount == destroyNumber)
						GameObject.Destroy (gameObject);
	
	}
}
