using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private List<int> pinFalls = new List<int>();

	private PinSetter pinSetter;
	private Ball ball;

	// Use this for initialization
	void Start() {

		pinSetter = GameObject.FindObjectOfType<PinSetter>();
		ball = GameObject.FindObjectOfType<Ball>();
	
	}

	public void Bowl( int pinFall ) {

		// add this pinFall to the list
		pinFalls.Add( pinFall );

		// get the next action, given the state of the list
		ActionMaster.Action nextAction = ActionMaster.NextAction( pinFalls );

		// coordinate actions
		pinSetter.PerformAction( nextAction );
		ball.Reset();

	}

}
