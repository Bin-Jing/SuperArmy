using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float moveSpeed = 3f;

	public float jumpForce = 600f;

	public int CoinScore = 10;

	public int playerHealth = 1;

	public GameObject Bullet;

	public LayerMask whatIsGround;

	public Transform groundCheck;

	[HideInInspector]
	public bool playerCanMove = true;

	public AudioClip deathSFX;
	public AudioClip jumpSFX;
	public AudioClip coinSFX;

	Transform _transform;
	Rigidbody2D _rigidbody;
	Animator _animator;
	AudioSource _audio;

	float _vx;
	float _vy;

	bool isGrounded = false;
	bool isRunning = false;
	bool canShoot = true;
	bool doubleJump = false;
	bool isDead = false;
	int _playerLayer;

	void Awake () {
		_transform = GetComponent<Transform> ();

		_rigidbody = GetComponent<Rigidbody2D> ();
		if (_rigidbody==null) 
			Debug.LogError("Rigidbody2D component missing from this gameobject");
		
		_animator = GetComponent<Animator>();
		if (_animator==null) 
			Debug.LogError("Animator component missing from this gameobject");
		
		_audio = GetComponent<AudioSource> ();
		if (_audio==null) { 
			_audio = gameObject.AddComponent<AudioSource>();
		}
			
		_playerLayer = this.gameObject.layer;
	}

	void Update(){
		if (isDead||!playerCanMove || (Time.timeScale == 0f)) {
			_vx = 0;
			return;
		}
			
		//_vx = Input.GetAxisRaw ("Horizontal");
		_vx = 1;

		if (_vx != 0&& playerCanMove&& !isDead){
			isRunning = true;
		} else {
			isRunning = false;
		}

		_animator.SetBool("Running", isRunning);

		// get the current vertical velocity from the rigidbody component
		_vy = _rigidbody.velocity.y;

		isGrounded = Physics2D.Linecast(_transform.position, groundCheck.position, whatIsGround);  

		// Set the grounded animation states
		_animator.SetBool("Grounded", isGrounded);

		if((isGrounded || !doubleJump) && Input.GetButtonDown("Jump")){

			_vy = 0f;

			_rigidbody.velocity = new Vector2 (_rigidbody.velocity.x, 0);

			// add a force in the up direction
			_rigidbody.AddForce (new Vector2 (0, jumpForce));
			PlaySound(jumpSFX);

			if(!isGrounded){
				doubleJump = true;
			}
		
		}

		if (Input.GetButtonDown ("Fire1")&& canShoot) {
			Instantiate (Bullet, transform.position, new Quaternion(10, 0, 0, 0));
		}



		// If the player stops jumping mid jump and player is not yet falling
		// then set the vertical velocity to 0 (he will start to fall from gravity)
		if(Input.GetButtonUp("Jump") && _vy>0f){
			_vy = 0f;
		}

		// Change the actual velocity on the rigidbody
		_rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy);

	}
	void OnTriggerEnter2D (Collider2D other) {
		
		if (other.gameObject.tag == "Coin"&& !isDead) {
			PlaySound(coinSFX);
			GameObject.Find ("GameManager").GetComponent<GameManager> ().addScore (CoinScore);
			Destroy (other.gameObject);
		}

	}
	// Checking to see if the sprite should be flipped
	// this is done in LateUpdate since the Animator may override the localScale
	// this code will flip the player even if the animator is controlling scale
	void LateUpdate(){
		Vector3 localScale = _transform.localScale;
		if (isGrounded) {
			doubleJump = false;
		}
		_transform.localScale = localScale;
	}

	//freeze the player
	void FreezeMotion() {
		_rigidbody.velocity = new Vector2(0,0);
		playerCanMove = false;
		_rigidbody.isKinematic = true;
		canShoot = false;
	}

	//unfreeze the player
	void UnFreezeMotion() {
		playerCanMove = true;
		_rigidbody.isKinematic = false;
		canShoot = true;
	}

	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip){
		_audio.PlayOneShot(clip);
	}

	// public function to apply damage to the player
	public void ApplyDamage (int damage) {
		if (playerCanMove) {
			playerHealth -= damage;

			if (playerHealth <= 0) {
				StartCoroutine (KillPlayer ());
			}
		}
	}

	// public function to kill the player when they have a fall death
	public void FallDeath () {
		if (playerCanMove) {
			playerHealth = 0;
			StartCoroutine (KillPlayer ());
		}
	}

	// coroutine to kill the player
	IEnumerator KillPlayer(){
		isDead = true;
		FreezeMotion ();
		PlaySound(deathSFX);
		_animator.SetTrigger("Death");
		yield return new WaitForSeconds(2.0f);

//		Application.LoadLevel(Application.loadedLevelName);
		Application.LoadLevel(2);
		
	}

	public void Respawn(Vector3 spawnloc) {
		playerHealth = 1;
		UnFreezeMotion ();
		_transform.parent = null;
		_transform.position = spawnloc;
		_animator.SetTrigger("Respawn");
	}
}
