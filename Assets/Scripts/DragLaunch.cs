using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Ball) )]
public class DragLaunch : MonoBehaviour {

	private Ball ball;

	// Use this for initialization
	void Start() {

		ball = GetComponent<Ball>();
	
	}

	public void DragStart() {



	}

	public void DragEnd() {



	}
	
	// Update is called once per frame
	void Update() {
	
	}
}
