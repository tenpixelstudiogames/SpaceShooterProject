using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text _scoreText;
    public Text _highscoreText;

    private int _score;
    private static int _highscore;
    private void Awake()
    {
        instance = this;
        UpdateHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = "Your Score: " + _score.ToString();
        
    }

    public void UpdateHighScore()
    {
        if (_highscore < _score)
        {
            _highscoreText.text = "HighScore: " + _score.ToString();
            _highscore = _score;
        }
        else
        {
            _highscoreText.text = "HighScore: " + _highscore.ToString();
        }
    }
}
