using System.Collections;
using UnityEngine;

public class PlaneVanish : MonoBehaviour{
	
	public float secondsDelay;
	
	public float destroyTime = 30f;
	
	public float shrinkSpeed = 0.1f;	
	
	void Update(){
		if (Time.time >= secondsDelay){
			gameObject.transform.localScale *= shrinkSpeed * Time.deltaTime;
		}
		if (Time.time >= destroyTime){
			GameObject.Destroy(gameObject);
		}
	}
}