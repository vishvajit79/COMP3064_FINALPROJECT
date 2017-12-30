using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasController : MonoBehaviour {

	//some variables
	[SerializeField]
	public Text TitleLabel;
	[SerializeField]
	public Text MenuLabel;
	[SerializeField]
	public Text TimerLabel;
	[SerializeField]
	public Text LifeLabel;
	[SerializeField]
	public Text CurrentScoreLabel;
	[SerializeField]
	public Text HighScoreLabel;
	[SerializeField]
	public Button Level1Button;
	[SerializeField]
	public Button Level2Button;
	[SerializeField]
	public Button Level3Button;
	[SerializeField]
	public Text Level1ButtonText;
	[SerializeField]
	public Text Level2ButtonText;
	[SerializeField]
	public Text Level3ButtonText;
	[SerializeField]
	public Button QuitButton;
	[SerializeField]
	public Text QuitButtonText;
	[SerializeField]
	public Button PauseButton;
	[SerializeField]
	public Text PauseButtonText;

	public static CanvasController Instance;
	


	// Use this for initialization
	void Start () {
		
		Player.Instance.CanvasController = this;
		StartCounting();
		Player.Instance.Score = 0;
		Player.Instance.Life = 5;
		Player.Instance.Timer = 20;
		if (PlayerPrefs.GetInt("isDead") == 1)
		{
			GameStart();
			PlayerPrefs.SetInt("isDead", 0);
		}
		else
		{
			Initialize();
		}
	}

	private void StartCounting()
	{
		InvokeRepeating("Count", 0, 1);
	}

	public void Count() {
		if(Player.Instance.Timer > 0)
		{
			Player.Instance.Timer--;
			TimerLabel.text = "Timer: " + Player.Instance.Timer;
		}
		else
		{
			if (Player.Instance.Life != 0)
			{
				Player.Instance.Life--;
			}
			else
			{
				GameOver();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		PlayerPrefs.Save();
		if (Input.GetKey(KeyCode.Escape))
			if(!QuitButton.gameObject.activeSelf)
				GamePause();

 
	}

	private void Initialize()
	{
		Time.timeScale = 0;
		Player.Instance.Score = 0;
		Player.Instance.Life = 5;
		Player.Instance.Timer = 20;
		//gets high score from previous playerprefs save data
		Player.Instance.HighScore = PlayerPrefs.GetInt("highScore");
		TitleLabel.gameObject.SetActive(true);
		MenuLabel.gameObject.SetActive(true);
		TimerLabel.gameObject.SetActive(false);
		LifeLabel.gameObject.SetActive(false);
		CurrentScoreLabel.gameObject.SetActive(false);
		HighScoreLabel.gameObject.SetActive(true);
		Level1Button.gameObject.SetActive(true);
		Level1ButtonText.gameObject.SetActive(true);
		Level2Button.gameObject.SetActive(true);
		Level2ButtonText.gameObject.SetActive(true);
		Level3Button.gameObject.SetActive(true);
		Level3ButtonText.gameObject.SetActive(true);
		QuitButton.gameObject.SetActive(true);
		QuitButtonText.gameObject.SetActive(true);
		PauseButton.gameObject.SetActive(false);
		PauseButton.gameObject.SetActive(false);
	}

	public void GameStart()
	{
		Time.timeScale = 1;
		Player.Instance.Life = 5;
		Player.Instance.Timer = 20;
		//gets high score from previous save data
		Player.Instance.HighScore = PlayerPrefs.GetInt("highScore");
		TitleLabel.gameObject.SetActive(false);
		MenuLabel.gameObject.SetActive(false);
		TimerLabel.gameObject.SetActive(true);
		LifeLabel.gameObject.SetActive(true);
		CurrentScoreLabel.gameObject.SetActive(true);
		HighScoreLabel.gameObject.SetActive(false);
		Level1Button.gameObject.SetActive(false);
		Level1ButtonText.gameObject.SetActive(false);
		Level2Button.gameObject.SetActive(false);
		Level2ButtonText.gameObject.SetActive(false);
		Level3Button.gameObject.SetActive(false);
		Level3ButtonText.gameObject.SetActive(false);
		QuitButton.gameObject.SetActive(false);
		QuitButtonText.gameObject.SetActive(false);
		PauseButton.gameObject.SetActive(false);
		PauseButton.gameObject.SetActive(false);
	}

	public void GamePause()
	{
		Time.timeScale = 0;
		TitleLabel.gameObject.SetActive(true);
		MenuLabel.gameObject.SetActive(true);
		TimerLabel.gameObject.SetActive(true);
		LifeLabel.gameObject.SetActive(true);
		CurrentScoreLabel.gameObject.SetActive(true);
		HighScoreLabel.gameObject.SetActive(true);
		Level1Button.gameObject.SetActive(false);
		Level1ButtonText.gameObject.SetActive(false);
		Level2Button.gameObject.SetActive(false);
		Level2ButtonText.gameObject.SetActive(false);
		Level3Button.gameObject.SetActive(false);
		Level3ButtonText.gameObject.SetActive(false);
		QuitButton.gameObject.SetActive(true);
		QuitButtonText.gameObject.SetActive(true);
		PauseButton.gameObject.SetActive(true);
		PauseButton.gameObject.SetActive(true);
		MenuLabel.text = "Game Paused";
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		PlayerPrefs.Save();
		TitleLabel.gameObject.SetActive(true);
		MenuLabel.gameObject.SetActive(true);
		TimerLabel.gameObject.SetActive(false);
		LifeLabel.gameObject.SetActive(false);
		CurrentScoreLabel.gameObject.SetActive(true);
		HighScoreLabel.gameObject.SetActive(true);
		Level1Button.gameObject.SetActive(true);
		Level1ButtonText.gameObject.SetActive(true);
		Level2Button.gameObject.SetActive(true);
		Level2ButtonText.gameObject.SetActive(true);
		Level3Button.gameObject.SetActive(true);
		Level3ButtonText.gameObject.SetActive(true);
		QuitButton.gameObject.SetActive(true);
		QuitButtonText.gameObject.SetActive(true);
		PauseButton.gameObject.SetActive(false);
		PauseButton.gameObject.SetActive(false);
		MenuLabel.text = "Game Over\nPlease select the level you want to play";
		PlayerPrefs.SetInt("isDead", 1);
	}

	public void UpdateUi()
	{
		LifeLabel.text = "Life: " + Player.Instance.Life;
		TimerLabel.text = "Timer: " + Player.Instance.Timer;
		CurrentScoreLabel.text = "Current Score: " + Player.Instance.Score;
		HighScoreLabel.text = "High Score: " + Player.Instance.HighScore;
		PlayerPrefs.Save();
	}

	//this method will close the application
	public void QuitButtonClick()
	{
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}

	public void Level1ButtonClick()
	{
		if (PlayerPrefs.GetInt("isDead") == 1)
		{
			PlayerPrefs.SetInt("isDead", 1);
			SceneManager.LoadScene("main");
		}
		else
		{
			GameStart();
		}
	   
	}

	public void Level2ButtonClick()
	{
		// Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;
		if (sceneName == "level2")
		{
			if (PlayerPrefs.GetInt("isDead") == 1)
			{
				PlayerPrefs.SetInt("isDead", 1);
				SceneManager.LoadScene("level2");
			}
			else
			{
				GameStart();
			}
		}
		else
		{
			SceneManager.LoadScene("level2");
		}

	}

	public void Level3ButtonClick()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ResumeButtonClick()
	{
		Time.timeScale = 1;
		TitleLabel.gameObject.SetActive(false);
		MenuLabel.gameObject.SetActive(false);
		TimerLabel.gameObject.SetActive(true);
		LifeLabel.gameObject.SetActive(true);
		CurrentScoreLabel.gameObject.SetActive(true);
		HighScoreLabel.gameObject.SetActive(false);
		Level1Button.gameObject.SetActive(false);
		Level1ButtonText.gameObject.SetActive(false);
		Level2Button.gameObject.SetActive(false);
		Level2ButtonText.gameObject.SetActive(false);
		Level3Button.gameObject.SetActive(false);
		Level3ButtonText.gameObject.SetActive(false);
		QuitButton.gameObject.SetActive(false);
		QuitButtonText.gameObject.SetActive(false);
		PauseButton.gameObject.SetActive(false);
		PauseButton.gameObject.SetActive(false);
	}
}
