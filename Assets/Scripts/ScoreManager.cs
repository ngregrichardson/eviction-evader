using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	#region Singleton
	public static ScoreManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public int score = 0;
	public Text scoreText;
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "You have stopped " + score + " people from taking your land!";
	}

	public void Increase()
	{
		score++;
	}
}
