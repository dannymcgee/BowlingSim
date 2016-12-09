using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	public GameObject moveUI;
	public GameObject aimUI;
	public GameObject spinUI;

	public enum ControlMode {
		Move,
		Aim,
		Spin,
		Locked}

	;

	public ControlMode currentMode;

	private Ball ball;
	private float sensFactor = 1f;
	private CameraControl cameraControl;

	private Vector3 launchVelocity = Vector3.forward;
	private float velocityMax = 1000f;
	private float velocityMin = 150f;

	private Vector3 launchAngularVelocity = Vector3.zero;
	private float angularVelocityMax = 30f;
	private float angularVelocityMin = -30f;

	private Vector3 uiAngularVelocity;
	private Rigidbody spinUIrb;

	// Use this for initialization
	void Start() {

		ball = GameObject.FindObjectOfType<Ball>();
		cameraControl = GameObject.FindObjectOfType<CameraControl>();

		spinUIrb = spinUI.GetComponent<Rigidbody>();
		uiAngularVelocity = launchAngularVelocity;

		SetMode( ControlMode.Move );
	
	}
	
	// Update is called once per frame
	void Update() {

		if( currentMode == ControlMode.Move ) {
			UpdateMove();
		}
		else if( currentMode == ControlMode.Aim ) {
			UpdateAim();
		}
		else if( currentMode == ControlMode.Spin ) {
			UpdateSpin();
		}
		else if( currentMode == ControlMode.Locked ) {
			UpdateLocked();
		}
		else {
			Debug.LogWarning( "InputController doesn't know how to handle ControlMode: " + currentMode );
		}
	
	}

	public void SetMode( ControlMode mode ) {
		
		currentMode = mode;

		if( currentMode == ControlMode.Move ) {
			
			moveUI.SetActive( true );
			aimUI.SetActive( false );
			spinUI.SetActive( false );
			cameraControl.SetMode( CameraControl.CameraMode.FirstPerson );

		}
		else if( currentMode == ControlMode.Aim ) {
			
			moveUI.SetActive( false );
			aimUI.SetActive( true );
			spinUI.SetActive( false );
			cameraControl.SetMode( CameraControl.CameraMode.Overhead );

		}
		else if( currentMode == ControlMode.Spin ) {
			
			moveUI.SetActive( false );
			aimUI.SetActive( false );
			spinUI.SetActive( true );
			cameraControl.SetMode( CameraControl.CameraMode.FirstPerson );

		}
		else if( currentMode == ControlMode.Locked ) {
			
			moveUI.SetActive( false );
			aimUI.SetActive( false );
			spinUI.SetActive( false );
			cameraControl.SetMode( CameraControl.CameraMode.Locked );

		}

	}



	public void CycleModeNext() {
		// MOVE -> AIM -> SPIN
		if( currentMode == ControlMode.Move ) {
			SetMode( ControlMode.Aim );
		}
		else if( currentMode == ControlMode.Aim ) {
			SetMode( ControlMode.Spin );
		}
		else if( currentMode == ControlMode.Spin ) {
			SetMode( ControlMode.Move );
		}
	}

	public void CycleModeBack() {
		// MOVE -> SPIN -> AIM
		if( currentMode == ControlMode.Move ) {
			SetMode( ControlMode.Spin );
		}
		else if( currentMode == ControlMode.Aim ) {
			SetMode( ControlMode.Move );
		}
		else if( currentMode == ControlMode.Spin ) {
			SetMode( ControlMode.Aim );
		}
	}

	public void LaunchBall() {
		SetMode( ControlMode.Locked );
		cameraControl.SetMode( CameraControl.CameraMode.Chase );
		ball.Launch( launchVelocity, launchAngularVelocity );
	}



	public void ResetLaunchVelocity() {
		launchVelocity = new Vector3( 0f, 0f, velocityMin );
		launchAngularVelocity = Vector3.zero;
		spinUI.transform.rotation = Quaternion.identity;
	}

	void UpdateMove() {

		Vector3 ballPos = ball.transform.position;

		// right-click and drag to move the ball left/right
		if( Input.GetButton( "Fire2" ) ) {
			Cursor.lockState = CursorLockMode.Locked;
			ballPos.x += Input.GetAxis( "Mouse X" ) * sensFactor;
			ballPos.x = Mathf.Clamp( ballPos.x, -50f, 50f );
			ball.transform.position = ballPos;
		}
		else {
			Cursor.lockState = CursorLockMode.None;
		}

		if( Input.GetButtonDown( "Fire1" ) ) {
			// SetMode();
		}

	}

	void UpdateAim() {

		// right-click and drag to change the launch velocity
		if( Input.GetButton( "Fire2" ) ) {

			// lock cursor
			Cursor.lockState = CursorLockMode.Locked;

			// handle x axis
			launchVelocity.x += Input.GetAxis( "Mouse X" ) * sensFactor;
			launchVelocity.x = Mathf.Clamp( launchVelocity.x, -50f, 50f );

			// handle y axis
			launchVelocity.z += Input.GetAxis( "Mouse Y" ) * 10f;
			launchVelocity.z = Mathf.Clamp( launchVelocity.z, velocityMin, velocityMax );

			// debug
			Debug.Log( launchVelocity );

		}
		else {
			// unlock cursor
			Cursor.lockState = CursorLockMode.None;
		}

		// change the UI graphics to reflect the launch velocity
		GameObject arrowRotationPivot = aimUI;
		GameObject arrowZScalePivot = GameObject.Find( "AimArrowScalePivot" );

		float zScale;

		float zScaleMin = 1f;
		float zScaleMax = 10f;

		float zScaleLerp = (launchVelocity.z - velocityMin) / (velocityMax - velocityMin);

		zScale = Mathf.Lerp( zScaleMin, zScaleMax, zScaleLerp );

		arrowRotationPivot.transform.rotation = Quaternion.LookRotation( new Vector3( launchVelocity.x * 1.3f, launchVelocity.y, launchVelocity.z ), Vector3.up );
		arrowZScalePivot.transform.localScale = new Vector3( 1f, 1f, zScale );
		
	}

	void UpdateSpin() {

		// right-click and drag to change the angular velocity
		if( Input.GetButton( "Fire2" ) ) {

			// lock cursor
			Cursor.lockState = CursorLockMode.Locked;

			// handle x axis
			launchAngularVelocity.z += Input.GetAxis( "Mouse X" ) * -sensFactor;
			launchAngularVelocity.z = Mathf.Clamp( launchAngularVelocity.z, angularVelocityMin, angularVelocityMax );

			// handle y axis
			launchAngularVelocity.x += Input.GetAxis( "Mouse Y" ) * sensFactor;
			launchAngularVelocity.x = Mathf.Clamp( launchAngularVelocity.x, angularVelocityMin, angularVelocityMax );

			Debug.Log( launchAngularVelocity );

		}
		else {
			// unlock cursor
			Cursor.lockState = CursorLockMode.None;
		}

		// change the UI graphics to reflect the angular velocity
		spinUIrb.maxAngularVelocity = Mathf.Infinity;
		uiAngularVelocity = launchAngularVelocity * 0.5f;
		spinUIrb.angularVelocity = uiAngularVelocity;
		
	}

	void UpdateLocked() {
		
	}

}
