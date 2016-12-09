using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

	public Text[] rollTexts, frameTexts;

	// Use this for initialization
	void Start() {
		
		rollTexts[0].text = "X";
		frameTexts[0].text = "10";

		foreach( Text rollText in rollTexts ) {
			rollText.text = "";
		}
		foreach( Text frameText in frameTexts ) {
			frameText.text = "";
		}

	}

	// fill the roll scores on the scorecard
	public void FillRolls( List<int> rolls ) {
		string scoresString = FormatRolls( rolls );
		for( int i = 0; i < scoresString.Length; i++ ) {
			rollTexts[i].text = scoresString[i].ToString();
		}
	}

	// fill the frame scores on the scorecard
	public void FillFrames( List<int> frames ) {
		for( int i = 0; i < frames.Count; i++ ) {
			frameTexts[i].text = frames[i].ToString();
		}
	}

	// format rolls into string ready for UI
	public static string FormatRolls( List<int> rolls ) {
		
		string output = "";
		int strikeOffset = 0;

		for( int i = 0; i < rolls.Count; i++ ) {
			
			int rollNumber = i + 1 + strikeOffset;

			if( rollNumber % 2 == 0 && rolls[i - 1] + rolls[i] == 10 ) {
				// this is an even-numbered roll, and this + the last roll = 10
				// That's a SPARE
				output += "/";
			}
			else if( rolls[i] == 10 ) {
				// this is an odd-numbered roll for 10
				// That's a STRIKE
				if( rollNumber >= 19 ) {
					// we're in the last frame
					output += "X";														
				}
				else {
					// we're in a normal frame
					output += "X ";														
				}
				// this helps us keep track of which roll we're on
				strikeOffset++;
			}
			else if( rolls[i] == 0 ) {
				// replace zeros with dashes
				output += "-";
			}
			else {
				// not a strike, not a spare, and not zero,
				// so just output the value
				output += rolls[i];
			}
		}
		return output;
		
	}
	
	// Update is called once per frame
	void Update() {
	
	}
}
