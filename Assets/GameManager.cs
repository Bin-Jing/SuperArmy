using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
//	public Text scoreText;
	int score = 0;
	// Use this for initialization
	void Start () {
		
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDisable(){
		PlayerPrefs.SetInt ("Score", score);
	}
	public void addScore(int n){
		score += n;
//		scoreText.text = "Score : " + score;

	}
	void OnGUI(){
		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.normal.textColor = Color.white;
		GUI.Label (new Rect (0, 0, 100, 30), "Score : " + score, style);
	}
}
