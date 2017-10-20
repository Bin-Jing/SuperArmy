using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 2 - 20, Screen.height-100, 60, 30), "Back")) {
			Application.LoadLevel (0);
		}
	}
}
