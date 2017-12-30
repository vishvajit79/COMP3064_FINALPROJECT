using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	Transform player = null;
	[SerializeField]
	private float leftX;
	[SerializeField]
	private float rightX;
	[SerializeField]
	private float topY;
	[SerializeField]
	private float bottomY;
	private Vector3 _currentPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    Debug.Assert(player != null, "player != null");
	    gameObject.transform.position = 
			new Vector3 (
				player.position.x,
				player.position.y,
				gameObject.transform.position.z
			);


		_currentPos = gameObject.transform.position;
		CheckBounds ();
		gameObject.transform.position = _currentPos;

	}

	private void CheckBounds(){

		if (_currentPos.x < leftX) {
			_currentPos.x = leftX;
		}

		if (_currentPos.x > rightX) {
			_currentPos.x = rightX;
		}

		if (_currentPos.y > topY) {
			_currentPos.y = topY;
		}

		if (_currentPos.y < bottomY) {
			_currentPos.y = bottomY;
		}

	}
}
