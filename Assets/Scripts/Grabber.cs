using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Grabber : MonoBehaviour {

	public GameObject pinsContainer;
	public GameObject pinSetPrefab;
	public GameObject grabberObject;

	private List<Pin> pinsList = new List<Pin>();
	private bool isRenewing = false;

	Vector3 targetPinsPosition;
	Vector3 spawnPinsPosition;
	Quaternion targetPinsRotation;


	// Use this for initialization
	void Start() {

		// find the position of the initial pins container
		// and set the position for new instance by raising it ~40cm
		targetPinsPosition = pinsContainer.transform.position;
		spawnPinsPosition = targetPinsPosition;
		spawnPinsPosition.y += 40.7255f;
		targetPinsRotation = pinsContainer.transform.rotation;
	
	}

	void OnTriggerEnter( Collider collider ) {
		// when a pin enters the grabber's collider,
		// add it to the grabbers pins so we know to grab it
		// when the grabber is activated
		if( collider.GetComponent<Pin>() ) {
			Pin pin = collider.GetComponent<Pin>();
			if( !pinsList.Contains( pin ) ) {
				pinsList.Add( pin );
			}
		}

	}

	void OnTriggerExit( Collider collider ) {
		// when a pin leaves the grabber's collider,
		// remove it from the grabber's list
		if( collider.GetComponent<Pin>() ) {
			Pin pin = collider.GetComponent<Pin>();
			if( pinsList.Contains( pin ) ) {
				pinsList.Remove( pin );
			}
		}

	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void GrabPins() {
		// retrieve each pin in the grabber's list
		foreach( Pin pin in pinsList ) {
			// re-parent the pins to the grabber object and neutralize their velocity and rotation
			// so they can be moved as a cohesive unit
			pin.transform.parent = grabberObject.transform;
			Rigidbody rb = pin.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.rotation = pin.originRotation;
			rb.Sleep();
		}
		if( isRenewing ) {
			// if the grabber is spawning new pins,
			// reset the pinsContainer to its target position
			// (i.e., not elevated)
			Invoke( "ResetPinsPosition", 0.2f );
			isRenewing = false;
		}
	}

	public void ReleasePins() {
		// retrieve each pin in the grabber's list
		foreach( Pin pin in pinsList ) {
			// re-parent the pins back to the pinsContainer
			// and re-enable their gravity
			if( pin != null ) {
				pin.transform.parent = pinsContainer.transform;
				Rigidbody rb = pin.GetComponent<Rigidbody>();
				rb.useGravity = true;
				rb.WakeUp();
			}
		}
		// clear the grabber's list of pins (just in case)
		pinsList.Clear();
	}

	public void RenewPins() {

		// destroy the old (now empty) pinsContainer
		Destroy( pinsContainer );

		// instantiate the new pin set and reassign the pinsContainer variable
		pinsContainer = (GameObject)Instantiate( pinSetPrefab, spawnPinsPosition, targetPinsRotation, GameObject.Find( "Pinsetter" ).transform );

		// add the new pins to the grabber's list
		foreach( Transform child in pinsContainer.transform ) {
			Pin pin = child.GetComponent<Pin>();
			if( !pinsList.Contains( pin ) ) {
				pinsList.Add( pin );
			}
		}

		// Invoke the grabber to grab the pins when they fall into the imaginary "notch"
		Invoke( "GrabPins", 0.15f );
		isRenewing = true;

	}

	void ResetPinsPosition() {
		pinsContainer.transform.position = targetPinsPosition;
	}

}
