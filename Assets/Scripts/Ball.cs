using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Vector3 launchVelocity;
	public Vector3 launchAngularVelocity;
	public bool inPlay = false;

	// public Vector3 launchTorque;
	private Rigidbody rigidBody;
	private AudioSource audioSource;

	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start() {
		
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

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
			if( !inPlay ) {
				Launch( launchVelocity, launchAngularVelocity );
			}
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
