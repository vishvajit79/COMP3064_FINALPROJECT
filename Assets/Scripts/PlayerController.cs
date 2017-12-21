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
	//public variables
	[SerializeField]
	CanvasController gameController;

	private Vector2 _currentPos;

	private Rigidbody2D _rigidBody = null;
	private Animator _animator = null;

	private AudioSource[] sounds;
	private AudioSource _hitSound;
	private AudioSource _jumpSound;
	private AudioSource _doorSound;
	private AudioSource _clockSound;


	// Use this for initialization
	void Start () {
		_animator = gameObject.GetComponent<Animator> ();
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();

		sounds = GetComponents<AudioSource>();
		_hitSound = sounds[0];
		_jumpSound = sounds[1];
		_doorSound = sounds[2];
		_clockSound = sounds[3];

	}

	private void Update()
	{
		
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
			if (_jumpSound != null)
			{
				_jumpSound.Play();
			}
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


		//hit
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("firing");
			_animator.SetBool("fire", true);
		}
		else {
			_animator.SetBool("fire", false);
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


	public void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "obstacle") {
			if (Input.GetKey(KeyCode.Space))
			{
				Debug.Log("Collision obstacle\n");
				if (_hitSound != null)
				{
					_hitSound.Play();
				}
				coll.gameObject.
				GetComponent<ObstacleManager>()
				.DestroyMe();
				Player.Instance.Score += 100;
			}
		}
		if (coll.gameObject.tag == "clock")
		{
			Debug.Log("Collision clock\n");
			if (_clockSound != null)
			{
				_clockSound.Play();
			}
			coll.gameObject.
			   GetComponent<ObstacleManager>()
			   .DestroyMe();
			Player.Instance.Timer += 10;
		}
		if (coll.gameObject.tag == "door")
		{
			Debug.Log("Collision door\n");
			if (_doorSound != null)
			{
				_doorSound.Play();
			}
			Destroy(gameObject);
			gameController.GameOver();
			gameController.MenuLabel.text = "Level 1 finished - Click on Level 2 to play next level";
		}
	}

}
