using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoreMaster {

	// takes the list of each frame's score and adds them together
	// to return the running total for each frame
	public static List<int> ScoreCumulative( List<int> rolls ) {
		List<int> cumulativeScores = new List<int>();
		int runningTotal = 0;
		foreach( int frameScore in ScoreFrames(rolls) ) {
			runningTotal += frameScore;
			cumulativeScores.Add( runningTotal );
		}
		return cumulativeScores;
	}

	public static List<int> ScoreFrames( List<int> rolls ) {
		
		List<int> frameList = new List<int>();							// create a list to hold each frame's score

		for( int i = 1; i < rolls.Count; i += 2 ) {						// iterate every two rolls (i.e. at the end of each frame)

			if( frameList.Count == 10 ) {								// break the loop if we've reached frame 10
				break;
			}

			int frameTotal = rolls[i - 1] + rolls[i];					// add together this and the previous roll to get the frame total

			if( frameTotal < 10 ) {										// NORMAL FRAME
				frameList.Add( frameTotal );							// add the frame total to the list
			}

			if( rolls.Count - i <= 1 ) {								// insufficient lookahead,
				break;													// break for now
			}

			if( rolls[i - 1] == 10 ) {									// STRIKE
				i--;													// no 2nd roll on strike frame
				frameList.Add( 10 + rolls[i + 1] + rolls[i + 2] );		// add the two rolls' totals following the strike to this frame

			} else if( frameTotal == 10 ) {								// SPARE
				frameList.Add( 10 + rolls[i + 1] );						// add the next roll's total to this frame
			}
		}
		return frameList;												// return the list of frame scores

	}



}
