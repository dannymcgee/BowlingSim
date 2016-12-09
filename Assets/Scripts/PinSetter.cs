using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

	public Grabber grabber;

	private PinCounter pinCounter;
	private Animator animator;

	// Use this for initialization
	void Start() {

		grabber = FindObjectOfType<Grabber>();
		pinCounter = FindObjectOfType<PinCounter>();
		animator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void PerformAction( ActionMaster.Action action ) {
		
		// perform the tidy action
		if( action == ActionMaster.Action.Tidy ) {
			animator.SetTrigger( "tidyTrigger" );
		}

		// perform the reset action
		if( action == ActionMaster.Action.Reset || action == ActionMaster.Action.EndTurn ) {
			animator.SetTrigger( "resetTrigger" );
			pinCounter.Reset();
		}

		// end the game
		if( action == ActionMaster.Action.EndGame ) {
			animator.SetTrigger( "resetTrigger" );
			Invoke( "EndGame", 9f );
		}

	}

	void EndGame() {
		animator.enabled = false;
		GameObject pinLights = GameObject.Find( "PinLights" );
		pinLights.SetActive( false );
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
