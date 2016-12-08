using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	private List<int> pinFalls = new List<int>();

	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start() {

		pinSetter = GameObject.FindObjectOfType<PinSetter>();
		ball = GameObject.FindObjectOfType<Ball>();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
	
	}

	public void Bowl( int pinFall ) {

		// reset the ball
		ball.Reset();

		// add this pinFall to the list
		pinFalls.Add( pinFall );

		// get the next action, given the state of the list
		ActionMaster.Action nextAction = ActionMaster.NextAction( pinFalls );

		// coordinate actions
		pinSetter.PerformAction( nextAction );

		try {
			// mark the scorecard
			scoreDisplay.FillRollCard( pinFalls );
		} catch {
			Debug.LogError( "FillRollCard encountered an error!" );
		}


	}

}
