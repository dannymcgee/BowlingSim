using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public CameraControl Instance;

	public enum CameraMode {
		FirstPerson,
		Overhead,
		Chase,
		Locked

	}

	public CameraMode currentMode;

	private Camera cam;

	private Vector3 firstPersonPosition = new Vector3( 0f, 41f, -2038.38f );
	private Vector3 firstPersonEulerAngles = new Vector3( 3.29f, 0f, 0f );
	private float firstPersonFOV = 28f;

	private Vector3 overheadPosition = new Vector3( 0f, 135.5f, -2038.38f );
	private Vector3 overheadEulerAngles = new Vector3( 18.28f, 0f, 0f );
	private float overheadFOV = 40f;

	private Vector3 currentPosition;
	private Vector3 currentEulerAngles;

	private Ball ball;

	private float offsetZ;

	// Use this for initialization
	void Start() {

		Debug.Log( "firstPersonFOV: " + firstPersonFOV );

		Instance = this;
		ball = GameObject.FindObjectOfType<Ball>();
		cam = GetComponent<Camera>();
		Debug.Log( "cam: " + cam );

		// firstPersonPosition = transform.position;
		currentMode = CameraMode.FirstPerson;

		offsetZ = transform.position.z - ball.transform.position.z;
	
	}
	
	// Update is called once per frame
	void Update() {

		if( currentMode == CameraMode.Chase ) {
			UpdateChase();
		}


		// DEBUG
		// TODO - remove me
//		if( Input.GetButtonDown( "Fire2" ) ) {
//			if( currentMode == CameraMode.FirstPerson ) {
//				SetMode( CameraMode.Overhead );
//			} else {
//				SetMode( CameraMode.FirstPerson );
//			}
//		}

	}

	// reset the camera back to its original position
	public void Reset() {
		transform.position = firstPersonPosition;
	}

	public void SetMode( CameraMode mode ) {
		
		currentMode = mode;

		if( mode == CameraMode.FirstPerson ) {
			
			transform.position = firstPersonPosition;
			transform.rotation = Quaternion.Euler( firstPersonEulerAngles );
			Debug.Log( "firstPersonFOV: " + firstPersonFOV );
			Camera.main.fieldOfView = firstPersonFOV;

		}
		else if( mode == CameraMode.Overhead ) {
			
			transform.position = overheadPosition;
			transform.rotation = Quaternion.Euler( overheadEulerAngles );
			Camera.main.fieldOfView = overheadFOV;

		}
		else if( mode == CameraMode.Chase ) {

			Debug.Log( "Entered Chase Cam!" );
			transform.position = firstPersonPosition;
			transform.rotation = Quaternion.Euler( firstPersonEulerAngles );
			Camera.main.fieldOfView = firstPersonFOV;
			
		}
		else if( mode == CameraMode.Locked ) {

		}
	}

	void UpdateChase() {
		Debug.Log( "Updating Chase Cam!" );
		// chase the ball down the lane
		Vector3 newPosition = new Vector3( firstPersonPosition.x, firstPersonPosition.y, ball.transform.position.z + offsetZ );
		transform.position = newPosition;
		// stop when close to the pins
		if( ball.transform.position.z >= -100f ) {
			Debug.Log( "Locking Camera!" );
			SetMode( CameraMode.Locked );
		}
	}

}
