using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	private float speed = 1f;


	private Rigidbody2D _rigidBody;
	private Transform _transform;
	private bool isCollided = false;
    public PlayerController PlayerController;
	private float _width, _height;

	// Use this for initialization
	public void Start () {

		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		_transform = gameObject.transform;

		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer> ();
		_width = sprite.bounds.extents.x;
		_height = sprite.bounds.extents.y;

	}
	
	// Update is called once per frame
	public void FixedUpdate () {

		//Vector2 lineCastPos =
		//    (Vector2)_transform.position +
		//    (Vector2)_transform.right * _width -
		//    (Vector2)_transform.up * _height;

		//Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);

		if (isCollided)
		{

			Vector3 curRot = _transform.eulerAngles;
			curRot.y += 180;
			_transform.eulerAngles = curRot;
		}

		Vector2 vel = _rigidBody.velocity;
		vel.x = _transform.right.x * speed;
		_rigidBody.velocity = vel;

	}

	public void LateUpdate()
	{
		FixedUpdate();
	}

	public void Update()
	{
		FixedUpdate();
	}

	public void OnCollisionEnter2D(Collision2D collision2D)
	{
		if (collision2D.gameObject.tag == "obstacle" || collision2D.gameObject.tag == "random")
		{
			StartCoroutine(GodMod(0.01f));
		}

		if (collision2D.gameObject.tag == "player")
		{
			PlayerController.OnCollisionEnter2D(null);
		}
	}

	public IEnumerator GodMod(float x)
	{
		isCollided = true;
		yield return new WaitForSeconds(x);
		isCollided = false;
	}
}
