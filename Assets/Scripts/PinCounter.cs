using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCounter : MonoBehaviour {

	public Text standingDisplay;

	private GameManager gameManager;
	private bool ballEnteredBox = false;
	private float lastChangeTime;
	private int lastStandingCount = -1;
	private int lastSettledCount = 10;

	// Use this for initialization
	void Start() {
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}

	public void Reset() {
		lastSettledCount = 10;
	}

	void OnTriggerEnter( Collider collider ) {

		GameObject objectHit = collider.gameObject;
		if( objectHit.GetComponent<Ball>() ) {
			// look for fallen pins when the ball enters the trigger
			StartCounting();
		}

	}

	public void StartCounting() {
		
		ballEnteredBox = true;
		standingDisplay.color = Color.red;

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

		// report the result of this roll to the GameManager
		gameManager.Bowl( pinFall );

		// reset the lastStandingCount so that it will satisfy
		// CheckStandingCount's primary conditional on the next roll
		lastStandingCount = -1;
		standingDisplay.color = Color.green;

		// reset the flag to stop checking for fallen pins
		ballEnteredBox = false;

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

}
