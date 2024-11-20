using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    private static bool _canPause = true;

    private void Awake()
    {
        instance = this;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
      

        if (_canPause)
        {
            _canPause = false;            
            SceneManager.LoadScene("Pause_Menu", LoadSceneMode.Additive);
        }
    }

    public void ResumeGame()
    {
        
        SceneManager.UnloadSceneAsync("Pause_Menu");
        Time.timeScale = 1;
        _canPause = true;

    }

    public void DisplayMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
