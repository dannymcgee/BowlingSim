using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {

	public List<int> ScoreFrames( List<int> rolls ) {
		
		List<int> frameList = new List<int>();

		int i = 1;
		foreach( int roll in rolls ) {
			
			// roll #[i] knocked down [roll] pins

			i++;
		}

		return frameList;

	}

}
