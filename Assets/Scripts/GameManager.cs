using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	public float slowMoScale = 0.1f;
	public float realTimeScale = 1f;
	public float slowMoTransitionRate = 10f;
	public bool slowMoCameraEnabled = true;

	private float timeScale;
	private float slowMoLerpT = 1f;
	private bool slowMoActive = false;

	private List<int> rolls;
	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start() {

		// calculate physics 10x more precisely than default.
		// this gives us much more reliable and accurate bowling results
		// at the cost of much more expensive physics processing.

		// TODO -- make this adjustable via player prefs?
		Time.fixedDeltaTime = 0.02f * 0.1f;

		// initiate at realtime
		timeScale = realTimeScale;
		Time.timeScale = timeScale;

		// find all of the components we need
		pinSetter = GameObject.FindObjectOfType<PinSetter>();
		ball = GameObject.FindObjectOfType<Ball>();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();

		// create the list of rolls
		rolls = new List<int>();
	
	}

	// this gets called at the end of every roll
	public void Bowl( int pinFall ) {

		// add this pinFall to the list
		rolls.Add( pinFall );

		// get the next action, given the game history
		ActionMaster.Action nextAction = ActionMaster.NextAction( rolls );

		// cue the pinsetter to handle the game state
		pinSetter.PerformAction( nextAction );

		// reset the ball
		ball.Reset();

		// mark the scorecard
		try {
			// fill rolls
			scoreDisplay.FillRolls( rolls );
			// fill frames
			scoreDisplay.FillFrames( ScoreMaster.ScoreCumulative( rolls ) );
		} catch {
			Debug.LogError( "Something went wrong with scoreDisplay!" );
		}

	}

	void Update() {

		runSlowMo();

	}

	public void toggleSlowMo() {
		slowMoActive = !slowMoActive;
	}

	private void runSlowMo() {
		
		if( slowMoActive && slowMoCameraEnabled ) {
			// immediately switch to slow motion
			timeScale = slowMoScale;
			slowMoLerpT = 0f;

		} else {
			// lerp back to realtime
			if( slowMoLerpT < 1f ) {
				slowMoLerpT += slowMoTransitionRate * Time.deltaTime;
			} else {
				slowMoLerpT = 1f;
			}
			timeScale = Mathf.Lerp( slowMoScale, realTimeScale, slowMoLerpT );

		}
		Time.timeScale = timeScale;

	}

}
