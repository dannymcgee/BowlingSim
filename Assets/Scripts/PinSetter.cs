using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public int lastStandingCount = -1;
	public Text standingDisplay;
	public Grabber grabber;

	private bool ballEnteredBox = false;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private Ball ball;
	private ActionMaster actionMaster = new ActionMaster();
	private Animator animator;

	// Use this for initialization
	void Start() {

		grabber = FindObjectOfType<Grabber>();
		ball = FindObjectOfType<Ball>();
		animator = GetComponent<Animator>();
	
	}

	void OnTriggerEnter( Collider collider ) {
		
		GameObject objectHit = collider.gameObject;
		if( objectHit.GetComponent<Ball>() ) {
			// set flag when ball enters the pinsetting area
			ballEnteredBox = true;
			standingDisplay.color = Color.red;
		}

	}
	
	// Update is called once per frame
	void Update() {

		standingDisplay.text = CountStanding().ToString();

		if( ballEnteredBox ) {
			// check for fallen pins after the ball enters
			// the pinsetting area
			CheckStandingCount();
		}
	
	}

	void CheckStandingCount() {
		
		// check for changes in the number of standing pins since last update
		int currentStandingCount = CountStanding();
		if( currentStandingCount != lastStandingCount ) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStandingCount;
			return;
		}

		// if there are no changes for [settleTime] seconds,
		// call it settled
		float settleTime = 3f;
		if( Time.time - lastChangeTime >= settleTime ) {
			PinsHaveSettled();
		}
			
	}

	void PinsHaveSettled() {
		
		// get the number of pins knocked down this roll
		int pinFall = lastSettledCount - CountStanding();
		lastSettledCount = CountStanding();

		// get the action specified for the result of this roll
		ActionMaster.Action action = actionMaster.Bowl( pinFall );
		// action = ActionMaster.NextAction( TODO - list of pinFalls );
		Debug.Log( "PinFall: " + pinFall );

		// perform the tidy action
		if( action == ActionMaster.Action.Tidy ) {
			animator.SetTrigger( "tidyTrigger" );
		}

		// perform the reset action
		if( action == ActionMaster.Action.Reset || action == ActionMaster.Action.EndTurn ) {
			animator.SetTrigger( "resetTrigger" );
			lastSettledCount = 10;
		}

		// end the game
		if( action == ActionMaster.Action.EndGame ) {
			Debug.Log( "Game Over! What do I do now?" );
		}

		// reset the lastStandingCount so that
		// it will satisfy CheckStandingCount's
		// primary conditional on the next roll
		lastStandingCount = -1;
		standingDisplay.color = Color.green;

		// reset the flag to stop checking for fallen pins
		ballEnteredBox = false;

		// reset the ball's position for the next roll
		ball.Reset();

	}

	int CountStanding() {
		// query every pin in the scene to ask if it's standing
		// and return the count as an integer
		int standing = 0;
		foreach( Pin pin in GameObject.FindObjectsOfType<Pin>() ) {
			if( pin.IsStanding() ) {
				standing++;
			}
		}
		return standing;

	}


	// these "middle man" functions are only here
	// so that they can be accessed by the PinSetter's Animator Controller
	// is there a better way to do this?

	void GrabPins() {
		grabber.GrabPins();
	}

	void ReleasePins() {
		grabber.ReleasePins();
	}

	void RenewPins() {
		grabber.RenewPins();
	}

}
