using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public float speed = 2.0f;
	public Transform player;
	Animator _animator;
	Rigidbody2D _rigidbody;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (speed, 0, 0);
	}
	void OnCollisionEnter2D (Collision2D other) {
		
		if (other.gameObject.tag == "Monster") {
			StartCoroutine (BulletHit ());
			GameObject.Find ("GameManager").GetComponent<GameManager> ().addScore (1);
			other.gameObject.GetComponent<MonsterScript> ().MonsterDie ();

		}
		if (other.gameObject.tag == "Ground") {
			StartCoroutine (BulletHit ());
		}

	}
	IEnumerator BulletHit(){
		_rigidbody.isKinematic = false;
		speed = 0;
		_animator.SetTrigger("Bullethit");
		yield return new WaitForSeconds(0.3f);
		Destroy (this.gameObject);

	}
}
