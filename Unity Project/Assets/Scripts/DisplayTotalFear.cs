using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayTotalFear : MonoBehaviour {

	public Text thisText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		thisText.text = (FearLevel.totalFear.ToString());
	}
}
