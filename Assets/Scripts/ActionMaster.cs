using UnityEngine;
using System.Collections;

public class ActionMaster {

	public enum Action {
		Tidy,
		Reset,
		EndTurn,
		EndGame

	}

	private int[] bowls = new int[21];
	private int bowl = 1;

	public Action Bowl( int pins ) {

		// check for validity
		if( pins < 0 || pins > 10 ) {
			throw new UnityException( "Invalid pin count: " + pins.ToString() );
		}

		// get the number of pins for this roll
		bowls[bowl - 1] = pins;

		// end game if last bowl
		if( bowl == 21 || (bowl == 20 && !Bowl21Awarded()) ) {
			return Action.EndGame;
		}

		// if strike on first bowl of first frame, tidy on 2nd frame if < 10
		if( bowl == 20 && bowls[19 - 1] == 10 && pins < 10 ) {
			return Action.Tidy;
		}

		// if extra roll(s) awarded
		if( bowl >= 19 && Bowl21Awarded() ) {
			bowl++;
			return Action.Reset;
		}

		// 1st bowl of the frame
		if( bowl % 2 != 0 ) {

			// if strike
			if( pins == 10 ) {
				bowl += 2;
				return Action.EndTurn;
			}
			// if not strike
			if( pins < 10 ) {
				bowl++;
				return Action.Tidy;
			}

		}

		// 2nd bowl of the frame
		else {
			bowl++;
			return Action.EndTurn;
		}

		// all conditionals fail
		throw new UnityException( "Action.Bowl: All conditionals failed. What do?" );

	}

	private bool Bowl21Awarded() {
		return (bowls[19 - 1] + bowls[20 - 1] >= 10);
	}

}
