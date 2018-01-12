using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

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

    private AudioSource[] _sounds;
    private AudioSource _hitSound;
    private AudioSource _jumpSound;
    private AudioSource _doorSound;
    private AudioSource _clockSound;
    private AudioSource _obstacleSound;
	private AudioSource _explosionSound;

    private bool godMode = false;
    private int counter = 0;


    // Use this for initialization
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();

        _sounds = GetComponents<AudioSource>();
        _hitSound = _sounds[0];
        _jumpSound = _sounds[1];
        _doorSound = _sounds[2];
        _clockSound = _sounds[3];
        _obstacleSound = _sounds[4];
		_explosionSound = _sounds[5];

    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //walking
        Vector2 forceVect = new Vector2(
            Input.GetAxis("Horizontal"),
            0
        );
        _rigidBody.AddForce(forceVect * forceMultiplier);

        //jump
        float jump = Input.GetAxis("Jump");

        if (jump > 0 && IsGrounded())
        {
            if (_jumpSound != null)
            {
                _jumpSound.Play();
            }
            _rigidBody.AddForce(Vector2.up * jumpMultiplier);
        }


        _animator.SetInteger("velocity",
            (int)(Mathf.Abs(_rigidBody.velocity.x * 1000)));

        //flip
        if (_rigidBody.velocity.x > 0)
        {
            gameObject.transform.localScale = new Vector3(2, 2, 2);
        }
        else if (_rigidBody.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-2, 2, 2);
        }


        //hit
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("firing");
            _animator.SetBool("fire", true);
        }
        else
        {
            _animator.SetBool("fire", false);
        }


        _currentPos = gameObject.transform.position;
        CheckBounds();
        gameObject.transform.position = _currentPos;
        _animator.SetBool("jump", !IsGrounded());

    }

    private bool IsGrounded()
    {

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Vector2 pos = gameObject.transform.position;

        RaycastHit2D res = Physics2D.Linecast(
            new Vector2(pos.x, pos.y - (sr.bounds.size.y / 1.7f)),
            new Vector2(pos.x, pos.y - (sr.bounds.size.y / 2f))
        );

        Debug.Log(res + " " + res.collider);
        return res != null && res.collider != null;
    }

    private void CheckBounds()
    {

        if (_currentPos.x < leftX)
        {
            _currentPos.x = leftX;
        }

        if (_currentPos.x > rightX)
        {
            _currentPos.x = rightX;
        }

        if (_currentPos.y > topY)
        {
            _currentPos.y = topY;
        }
    }

    //flashes player after collision for 3 times
    public IEnumerator Blink()
    {
        Color c;
        Renderer renderer = gameObject.GetComponent<Renderer>();
        for (int i = 0; i < 1; i++)
        {
            for (float f = 1f; f >= 0; f -= 0.1f)
            {
                c = renderer.material.color;
                c.a = f;
                renderer.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
            for (float f = 0f; f <= 1; f += 0.1f)
            {
                c = renderer.material.color;
                c.a = f;
                renderer.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
        }

    }

    public IEnumerator GodMod(int x)
    {
        godMode = true;
        yield return new WaitForSeconds(x);
        godMode = false;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bomb")
        {
            Debug.Log("Collision bomb\n");
            if (_explosionSound != null)
            {
                _explosionSound.Play();
            }
            coll.gameObject.
            GetComponent<ObstacleManager>()
                .DestroyMe();
            StartCoroutine(Blink());
        }
        if (coll.gameObject.tag == "obstacle")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(GodMod(3));
                Debug.Log("Collision obstacle\n");
                if (_hitSound != null)
                {
                    _hitSound.Play();
                }
                coll.gameObject.GetComponent<ObstacleManager>()
                    .DestroyMe();
                Player.Instance.Score += 100;
            }
            else if (!godMode)
            {
                if (_obstacleSound != null)
                {
                    _obstacleSound.Play();
                }
                StartCoroutine(Blink());
                StartCoroutine(GodMod(3));
                Player.Instance.Life--;
            }
        }
		if (coll.gameObject.tag == "enemy")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                counter += 1;
                StartCoroutine(GodMod(3));
                Debug.Log("Collision enemy\n");
                if (_hitSound != null)
                {
                    _hitSound.Play();
                }
                if (counter == 2)
                {
                    coll.gameObject.GetComponent<ObstacleManager>()
                        .DestroyMe();
                    Player.Instance.Score += 100;
                }
                
            }
            else if (!godMode)
            {
                if (_obstacleSound != null)
                {
                    _obstacleSound.Play();
                }
                StartCoroutine(Blink());
                StartCoroutine(GodMod(3));
                Player.Instance.Life--;
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
            StartCoroutine(Blink());
        }
        if (coll.gameObject.tag == "door")
        {
            Debug.Log("Collision door\n");
            if (_doorSound != null)
            {
                _doorSound.Play();
            }
            Thread.Sleep(1000);
            Destroy(gameObject);
            Scene currentScene = SceneManager.GetActiveScene();

            // Retrieve the name of this scene.
            string sceneName = currentScene.name;
            if (sceneName == "main")
            {
                SceneManager.LoadScene("level2");
            }
            else if (sceneName == "level2")
            {
                SceneManager.LoadScene("level3");
            }
            else
            {
                gameController.GameOver();
                gameController.MenuLabel.text = "Congratulations!!! You have completed all the levels";
				gameController.RestartButton.gameObject.SetActive(true);
				gameController.RestartButtonText.gameObject.SetActive(true);
            }
        }
    }
}
