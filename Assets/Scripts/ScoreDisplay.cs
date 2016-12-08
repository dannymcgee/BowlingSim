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

	public void FillRollCard( List<int> rolls ) {

		for( int i = 1; i <= rolls.Count; i++ ) {

			rollTexts[i].text = rolls[i].ToString();
			
		}

	}
	
	// Update is called once per frame
	void Update() {
	
	}
}
