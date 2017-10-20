using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {



	// Handle gameobjects collider with a deathzone object
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<CharacterController2D> ().FallDeath ();
		} else {
			DestroyObject (other.gameObject);
		}

	}
	void OnTriggerEnter2D (Collider2D other) {
			DestroyObject (other.gameObject);

	}
}
