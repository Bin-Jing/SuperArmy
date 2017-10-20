using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height/2, 110, 30), "About this game")) {
			Application.LoadLevel (3);
		}
		GUI.Label (new Rect (Screen.width / 2 - 40, Screen.height/2 - 50, 200, 30), "Fire Button :Z");
		GUI.Label (new Rect (Screen.width / 2 - 40, Screen.height/2 -70, 200, 30), "Jump Button :X");
		if (GUI.Button (new Rect (Screen.width / 2 - 35, Screen.height-100, 60, 30), "Start")) {
			Application.LoadLevel (1);
		}
	}
}
