using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    //THis class takes care of the game over panel

    public Text timeTaken;
    public Image maskProgressBar;
    void Start()
    {
        //setting vales of progress bar and timer
        DisplayTime(CountDownTimeLeft.Instance.timeRemaining);
        float fillAmount = (float)ProgressBar.Instance.current / (float)ProgressBar.Instance.maximum;
        maskProgressBar.fillAmount = fillAmount;
    }

    //this method displays time in "{0:00}:{1:00}" format
    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //showing time in format
        timeTaken.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
