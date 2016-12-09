using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

	public float standingThreshold;
	private Vector3 eulerRotation;

	public Quaternion originRotation { get; protected set; }

	// Use this for initialization
	void Start() {

		// print( name + ": " + IsStanding() + "; xTilt: " + Mathf.Abs( eulerRotation.x ) + "; zTilt: " + eulerRotation.z ) );
		originRotation = transform.rotation;
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public bool IsStanding() {

		// get the rotation of the pin in eulerAngles
		eulerRotation = transform.rotation.eulerAngles;

		// reset X and Z to a default value of zero
		float xTilt = Mathf.Abs( 270 - eulerRotation.x );
		float zTilt = Mathf.Abs( eulerRotation.z );

		if( 
			// for X and Z, check whether we are within 3 degrees of 0 or 360
			(xTilt < standingThreshold || xTilt > (360 - standingThreshold)) &&
			(zTilt < standingThreshold || zTilt > (360 - standingThreshold)) ) {
			return true;
		} else {
			return false;
		}

	}

}
