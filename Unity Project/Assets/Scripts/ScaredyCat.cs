using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioClip))]
[RequireComponent(typeof(Collider))]
public class ScaredyCat : MonoBehaviour
{
    public GameObject fearMeter;
    Slider fearSlider;
    
	Animator animator;
	private float _fear = 0f;

    public float maxFear = 120f;
    public float fearDecrementAmount = 5f;
    public float fearFactor = 1f;
	float stopNoticingAt;

    Camera gameCamera;

    public float fear{ get { return _fear; } }

    public void beScared(float scariness)
    {
        float fright = scariness * fearFactor;
        _fear += fright;
        audio.Play();
		if (fear >= 60)
				animator.SetBool ("Jump", true);
		else
				animator.SetBool ("Curious", true);

		stopNoticingAt = Time.time + 0.63f;

    }
	void Awake ()
	{
		animator = transform.GetComponent<Animator> ();
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
		if (Time.time >= this.stopNoticingAt) {
			animator.SetBool ("Jump", false);
			animator.SetBool("Curious", false);
		}
    }

	void OnDestroy(){
		Destroy (fearMeter);
	}
}
