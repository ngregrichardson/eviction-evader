using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
	#endregion

	public GameObject loseText;
	public GameObject wintext;
	public GameObject startText;
	public Text subText;
	public bool isLost = false;
	public bool isWon = false;
	bool isStarting;
	float startTimer = 5;
	float timer = 3f;
	float timer2 = 3f;

	void Start()
	{
		isStarting = true;
		startText.SetActive(true);
		MenuManager.instance.crosshair.enabled = false;
	}

	void Update()
	{
		if(isStarting)
		{
			if(startTimer <= 0)
			{
				isStarting = false;
				startText.SetActive(false);
				MenuManager.instance.crosshair.enabled = true;
			}
			else
			{
				startTimer -= Time.deltaTime;
			}
		}
		if(isLost)
		{
			if(timer <= 0)
			{
				subText.text = "You stopped " + ScoreManager.instance.score + " people, but it just wasn't enough.";
				if(timer2 <= 0)
				{
					SceneManager.LoadScene(0);
				}else
				{
					timer2 -= Time.deltaTime;
				}
			}else
			{
				timer -= Time.deltaTime;
			}
		}

		if (isWon)
		{
			if (timer <= 0)
			{
				SceneManager.LoadScene(3);
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}

		if(Input.GetKeyDown(KeyCode.G))
		{
			Win();
		}
	}

	public void Lose ()
    {
		isLost = true;
		MenuManager.instance.crosshair.enabled = false;
		loseText.SetActive(true);
		FindObjectOfType<AudioManager>().Play("Lose");
	}

    public void Win()
    {
		isWon = true;
		MenuManager.instance.crosshair.enabled = false;
		wintext.SetActive(true);
		FindObjectOfType<AudioManager>().Play("Win");
	}
}
