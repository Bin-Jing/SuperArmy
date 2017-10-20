using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

	public int groundHealth = 2;
	int hit = 0;
	// Use this for initialization
	void Start () {
		hit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Bullet") {
			groundHealth--;
			if (groundHealth <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
}
