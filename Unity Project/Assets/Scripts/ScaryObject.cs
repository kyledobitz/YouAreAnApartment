using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class ScaryObject : MonoBehaviour {

	public class Effect{

		public Effect(float _cooldown, float _fearAmount, float _fearRequiredToUnlock){
			cooldown = _cooldown;
			fearAmount = _fearAmount;
			fearRequiredToUnlock = _fearRequiredToUnlock;

		}
		public GameObject prefab;
		public float cooldown;
		public float fearAmount;
		public float fearRequiredToUnlock;
		public float readyAt = 0.0f;
		public bool isActive;
		public bool canBeUsed;
	}


    
    public float effectRadius = 10f; //remove soon
    public Animation scarimation;
    public string fearEffect;
	public EffectName SpecificFearEffect = EffectName.DEVICE;
	
	public bool CHECK_THIS_BOX_FOR_BUTTONS;

	public static Dictionary<string, Effect> fearEffects = new Dictionary<string, Effect>();

	public static float COLDreadyTime = 0f;
	public static float LIGHTreadyTime = 0f;
	public static float DEVICEcreadyTime = 0f;
	public static float POSSESSIONreadyTime = 0f;

	public Effect thisEffect;


    public void Scare(){
        if (thisEffect.isActive && !this.CHECK_THIS_BOX_FOR_BUTTONS)
        {
			Debug.Log(gameObject.name + " used its fear effect");
            //PLAY ANIMATION. COSMO MAKE THIS WORK LOL

            Collider[] nearbyColliders = Physics.OverlapSphere(gameObject.transform.position, effectRadius);
			thisEffect.readyAt = Time.time + thisEffect.cooldown;
			thisEffect.isActive = false;
			Instantiate(thisEffect.prefab,gameObject.transform.position,Quaternion.identity);

            foreach (Collider coll in nearbyColliders)
            {
                if (coll.gameObject.tag == "Resident")
                {
                    coll.gameObject.GetComponent<ScaredyCat>().beScared(thisEffect.fearAmount);
                }
            }
        }
    }

	public enum EffectName{
		COLD,
		LIGHTS,
		DEVICE,
		FLING,
        POSSESSION
    }
    
    // Use this for initialization
	void Start () {

        switch (SpecificFearEffect) 
		{
			case EffectName.COLD:
				thisEffect = FearLevel.cold;
				break;
			case EffectName.LIGHTS:
				thisEffect = FearLevel.lights;
				break;
			case EffectName.DEVICE:
				thisEffect = FearLevel.device;
				break;
			case EffectName.FLING:
				thisEffect = FearLevel.fling;
				break;
			case EffectName.POSSESSION:
				thisEffect = FearLevel.possession;
	            break;
        }
    }
    
    // Update is called once per frame
	void Update () {
	
	}

}
