using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioClip))]
[RequireComponent(typeof(Collider))]
public class ScaredyCat : MonoBehaviour
{
    public GameObject fearMeter;
    Slider fearSlider;
    
	
	private float _fear = 0f;

    public float maxFear = 120f;
    public float fearDecrementAmount = 1f;
    public float fearFactor = 1f;

    Camera gameCamera;

    public float fear{ get { return _fear; } }

    public void beScared(float scariness)
    {
        float fright = scariness * fearFactor;
        _fear += fright;
        audio.Play();
    }

    // Use this for initialization
    void Start()
    {
        fearMeter = (GameObject) Instantiate(fearMeter);
        fearSlider = fearMeter.GetComponentInChildren<Slider>();
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fearSlider.minValue = 0;
        fearSlider.maxValue = maxFear;

    }

    // Update is called once per frame
    void Update()
    {
        fearSlider.transform.position = gameCamera.WorldToScreenPoint(gameObject.transform.position);

        fearSlider.value = fear;
        if (_fear > 0) 
		{
			_fear -= fearDecrementAmount * Time.deltaTime;
		}
    }

	void OnDestroy(){
		Destroy (fearMeter);
	}
}
