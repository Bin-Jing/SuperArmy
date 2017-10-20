using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArmyScripts : MonoBehaviour {

	public Transform player;
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x+10, 0, -5);
	}
}
