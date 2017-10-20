using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	public Text scoreText;
	int score = 0;
	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt ("Score");
	}
	
	void OnGUI(){
		scoreText.text = "Score : " + score;
		if (GUI.Button (new Rect (Screen.width / 2 + 20, Screen.height-80, 60, 30), "Retry?")) {
			Application.LoadLevel (1);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height-80, 60, 30), "Home")) {
			Application.LoadLevel (0);
		}
	}
}
