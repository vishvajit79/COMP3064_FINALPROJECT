using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float forceMultiplier = 1f;
	[SerializeField]
	private float jumpMultiplier = 30f;
	[SerializeField]
	private float leftX;
	[SerializeField]
	private float rightX;
	[SerializeField]
	private float topY;

	private Vector2 _currentPos;

	private Rigidbody2D _rigidBody = null;
	private Animator _animator = null;

	// Use this for initialization
	void Start () {
		_animator = gameObject.GetComponent<Animator> ();
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//walking
		Vector2 forceVect = new Vector2 (
			Input.GetAxis ("Horizontal"),
			0
		);
		_rigidBody.AddForce (forceVect * forceMultiplier);

		//jump
		float jump = Input.GetAxis("Jump");

		if (jump > 0 && IsGrounded()) {
			_rigidBody.AddForce (Vector2.up * jumpMultiplier);
		}


		_animator.SetInteger("velocity",
			(int)(Mathf.Abs(_rigidBody.velocity.x*1000)));

		//flip
		if (_rigidBody.velocity.x > 0) {
			gameObject.transform.localScale = new Vector3 (2, 2, 2);
		} else if (_rigidBody.velocity.x < 0)  {
			gameObject.transform.localScale = new Vector3 (-2, 2, 2);
		}

		_currentPos = gameObject.transform.position;
		CheckBounds ();
		gameObject.transform.position = _currentPos;
		_animator.SetBool ("jump", !IsGrounded ());

	}

	private bool IsGrounded(){

		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
		Vector2 pos = gameObject.transform.position;

		RaycastHit2D res = Physics2D.Linecast (
			new Vector2(pos.x,pos.y-(sr.bounds.size.y/1.7f)),
			new Vector2(pos.x,pos.y-(sr.bounds.size.y/2f))
		);

		Debug.Log (res + " " + res.collider );
		return res != null && res.collider != null;
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
	}
}
