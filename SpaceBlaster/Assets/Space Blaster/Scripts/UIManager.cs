using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesDisplay;
    public Text scoredisplay;
    public int score;
    public GameObject titleScreen;

    public void UpdateLives(int currentLives)
    {
        livesDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoredisplay.text = "SCORE: " + score;
    }
    public void ResetScore()
    {
        score = 0;
        scoredisplay.text = "SCORE: " + score;
    }
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }
    public void OfftitleScreen()
    {
        titleScreen.SetActive(false);
        ResetScore();
       
    }
    public void Exit()
    {
        Application.Quit();
    }
}
