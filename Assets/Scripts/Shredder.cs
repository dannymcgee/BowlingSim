using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {

	}

	void OnTriggerEnter( Collider collider ) {
		GameObject hitObject = collider.gameObject;
		
		if( hitObject.GetComponent<Ball>() ) {
			print( "Don't shred the ball!" );
			// Destroy( hitObject );
		} else if( hitObject.GetComponent<Pin>() ) {
			print( "Shredding Pin!" );
			Destroy( hitObject );
		}

	}

}
