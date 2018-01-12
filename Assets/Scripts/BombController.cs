using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

	/// PUBLIC INSTANCE VARIABLES
	public float speed = 0.5f;

	//PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	private Vector2 _currentPosition;
	[SerializeField]
	GameObject explosion;
    private AudioSource _explosionSound;

    // Use this for initialization
    void Start () {
		// Make a reference with the Transform Component
		this._transform = gameObject.GetComponent<Transform> ();
        _explosionSound = gameObject.GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {

		// reuse the object gem when it leaves the scene
		if (_currentPosition.y <= -2) {
			//this._transform.position= new Vector2(-50, 165);
			Reset();
		}

		// move gem in the scene 
		this._currentPosition = this._transform.position;
		this._currentPosition -= new Vector2(0, this.speed);
		this._transform.position = this._currentPosition;

	}

	//reset the gem and reuse the gem object
	public void Reset() {
		// code from my assignment 1
		this._transform.position = new Vector2 (Random.Range (-2, 42), 3.35f);
	}


	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag.Equals ("Player") || other.gameObject.tag.Equals ("ground")) {
            // create explosion

            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("bomb Collision with player");
                if (_explosionSound != null)
                {
                    _explosionSound.Play();
                }
                Player.Instance.Life -= 1;
            }
            else
            {
                Debug.Log("bomb collision with random");
            }
            Instantiate (explosion)
			.GetComponent<Transform> ()
			.position = other.gameObject
				.GetComponent<Transform> ()
				.position;
			Destroy (gameObject);
          
		}
	}
}
