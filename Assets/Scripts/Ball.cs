using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Vector3 launchVelocity;
	public Vector3 launchAngularVelocity;
	public bool inPlay = false;

	// public Vector3 launchTorque;
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private CameraControl cameraControl;

	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start() {

		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f * 0.1f;
		
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		cameraControl = GameObject.FindObjectOfType<CameraControl>();

		rigidBody.useGravity = false;
		rigidBody.maxAngularVelocity = Mathf.Infinity;

		startPosition = transform.position;
		startRotation = transform.rotation;

	}

	public void Launch( Vector3 velocity, Vector3 angularVelocity ) {
		
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;
		rigidBody.angularVelocity = angularVelocity;
		// rigidBody.AddTorque( launchTorque, ForceMode.Impulse );
		audioSource.Play();
		inPlay = true;

	}

	// Update is called once per frame
	void Update() {

		if( Input.GetButtonDown( "Fire1" ) ) {
			Launch( launchVelocity, launchAngularVelocity );
		}
	
	}

	public void Reset() {

		Debug.Log( "Resetting ball!" );
		inPlay = false;
		rigidBody.useGravity = false;
		transform.position = startPosition;
		transform.rotation = startRotation;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;

	}

}
