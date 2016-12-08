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

		// FIXME - this code doesn't function as intended. It's supposed to return true if the rotation
		// is within + or - 3 degrees of the standing rotation.
		//
		// Problem: negative rotation values are converted into their equivalent positives by Unity BEFORE
		// being accessed by IsStanding. I.e., a rotation of -0.1 is read as 359.9 by IsStanding, causing it
		// to return false even though it is well within the margin of error set by standingThreshold

		eulerRotation = transform.rotation.eulerAngles;
		float xTilt = Mathf.Abs( 270 - eulerRotation.x );
		float zTilt = Mathf.Abs( eulerRotation.z );

		if( (xTilt < standingThreshold) && (zTilt < standingThreshold) ) {
			return true;
		} else {
			return false;
		}

	}

}
