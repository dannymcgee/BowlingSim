using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ActionMaster {
	public enum Action {
		Tidy,
		Reset,
		EndTurn,
		EndGame,
		Undefined}

	;

	public static Action NextAction( List<int> rolls ) {
		Action nextAction = Action.Undefined;
		int strikeOffset = 0;

		for( int i = 0; i < rolls.Count; i++ ) { 				// Step through rolls
			
			int rollNumber = i + strikeOffset;

			if( rollNumber == 20 ) {
				nextAction = Action.EndGame;
			} else if( rollNumber >= 18 && rolls[i] == 10 ) { 	// Handle last-frame special cases
				nextAction = Action.Reset;
			} else if( rollNumber == 19 ) {
				if( rolls[i - 1] == 10 && rolls[i] == 0 ) {
					nextAction = Action.Tidy;
				} else if( rolls[i - 1] + rolls[i] == 10 ) {
					nextAction = Action.Reset;
				} else if( rolls[i - 1] + rolls[i] >= 10 ) {  	// Roll 21 awarded
					nextAction = Action.Tidy;
				} else {
					nextAction = Action.EndGame;
				}
			} else if( rollNumber % 2 == 0 ) { 					// First bowl of frame
				if( rolls[i] == 10 ) {
					strikeOffset++; 										
					nextAction = Action.EndTurn;
				} else {
					nextAction = Action.Tidy;
				}
			} else { 											// Second bowl of frame
				nextAction = Action.EndTurn;
			}
		}

		return nextAction;
	}
}