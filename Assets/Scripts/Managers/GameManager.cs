using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool _isGameOver = false;
    private void Awake()
    {
        instance = this;
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            MainMenuManager.instance.PauseGame();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
