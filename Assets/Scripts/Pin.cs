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
