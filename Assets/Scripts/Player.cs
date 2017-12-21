using UnityEngine;

public class Player {

    //public variable
    public CanvasController CanvasController;

    //private variables
    private int _score;
    private int _life = 2;
    private int _highScore;
    private int _timer = 20;
    

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
            CanvasController.UpdateUi();
            if (_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetInt("highScore", _highScore);
                CanvasController.UpdateUi();
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
                CanvasController.UpdateUi();
            }
            //sets highscore
            PlayerPrefs.SetInt("highScore", _highScore);
            CanvasController.UpdateUi();
        }

    }

    public int Timer
    {
        get { return _timer; }
        set
        {
            _timer = value;
            CanvasController.UpdateUi();
            if (_timer <= 0)
            {
                Instance.Life--;
                Instance.Timer = 30;
                CanvasController.UpdateUi();
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
                CanvasController.UpdateUi();
                CanvasController.GameOver();
            }
            else
            {
                //lifeLabel.text = "Life: " + _life;
                CanvasController.UpdateUi();
            }
        }
    }
}
