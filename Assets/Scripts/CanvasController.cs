using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Use this for initialization
    void Start () {
		this.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Escape))
            if(!QuitButton.gameObject.activeSelf)
	            GamePause();
    }

    private void Initialize()
    {
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
    }

    public void UpdateUi()
    {
        LifeLabel.text = "Life: ";
        TimerLabel.text = "Timer: ";
        CurrentScoreLabel.text = "Current Score: ";
        HighScoreLabel.text = "High Score: ";
    }

    //this method will close the application
    public void QuitButtonClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void Level1ButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Level2ButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Level3ButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeButtonClick()
    {
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
