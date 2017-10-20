using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {
	public int damge = 1;
	Animator _animator;
	bool isDead = false;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D (Collision2D other) {
		if (isDead) {
			this.tag = "DeadMonster";
		}
		if (other.gameObject.tag == "Player"&&!isDead) {
			// if player then tell the player to do its FallDeath
			other.gameObject.GetComponent<CharacterController2D> ().ApplyDamage(damge);
		}
	}

	public void MonsterDie(){
		StartCoroutine (KillMonster ());
	}
	IEnumerator KillMonster(){
		isDead = true;
		_animator.SetTrigger("MonsterDie");
		yield return new WaitForSeconds(0.5f);
		Destroy (this.gameObject);

	}
}
