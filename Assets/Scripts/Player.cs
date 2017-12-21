using UnityEngine;

public class Player {

    //public variable
    public CanvasController gameController;

    //private variables
    private int _score = 0;
    private int _life = 5;
    private int _highScore;
    private int _timer = 30;
    

    private static Player _instance; //declaring player instance

    //getter of player instance
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Player();
            }
            return _instance;
        }
    }

    //getter setter for score
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            gameController.UpdateUi();
            if (_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetInt("highScore", _highScore);
                gameController.UpdateUi();
            }
        }

    }
    //getter setter for highscore
    public int HighScore
    {
        get { return _highScore; }
        set
        {
            _highScore = value;
            //if current score is greater than previous high score, then sets high score to current score
            if (_score > _highScore)
            {
                _highScore = _score;
                gameController.UpdateUi();
            }
            //sets highscore
            PlayerPrefs.SetInt("highScore", _highScore);
            gameController.UpdateUi();
        }

    }

    public int Timer
    {
        get { return _timer; }
        set
        {
            _timer = value;
            gameController.UpdateUi();
            if (_timer <= 0)
            {
                gameController.UpdateUi();
                gameController.GameOver();
            }
        }

    }

    //getter setter for life
    public int Life
    {
        get { return _life; }
        set
        {
            _life = value;


            if (_life <= 0)
            {
                //game over
                _highScore = _score;
                gameController.UpdateUi();
                gameController.GameOver();
            }
            else
            {
                //lifeLabel.text = "Life: " + _life;
                gameController.UpdateUi();
            }
        }
    }
}
