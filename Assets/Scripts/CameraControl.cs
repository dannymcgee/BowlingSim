using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Ball ball;

	private float originX;
	private float originY;
	private float originZ;
	private float offsetZ;

	// Use this for initialization
	void Start() {

		originX = transform.position.x;
		originY = transform.position.y;
		originZ = transform.position.z;
		offsetZ = transform.position.z - ball.transform.position.z;
	
	}
	
	// Update is called once per frame
	void Update() {

		// "chase" the ball down the lane, but stop when the pins are full-frame
		if( ball.transform.position.z <= -100f ) {
			Vector3 newPosition = new Vector3( originX, originY, ball.transform.position.z + offsetZ );
			transform.position = newPosition;
		}

	
	}

	// reset the camera back to its original position
	public void Reset() {
		transform.position = new Vector3( originX, originY, originZ );
	}

}
