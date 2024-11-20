using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    private int _displaylives = 3;
    public Sprite[] images;
    public Image Lives;
    public Text GameOver;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Lives.sprite = images[3];
        GameOver.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int livesTooUpdate)
    {
        _displaylives = livesTooUpdate;
         Lives.sprite = images[_displaylives];
    }

    public void UpdateGameOver()
    {
        GameOver.text = "GameOver, Press R to Restart";
    }
}
