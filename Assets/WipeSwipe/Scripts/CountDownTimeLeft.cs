using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimeLeft : MonoBehaviour
{

    
    //This scripts counts the overall time left for player to finish the game

    [HideInInspector] public float timeRemaining; //Time left
    public static CountDownTimeLeft Instance;
    public bool timerIsRunning = false; //if timer is running
    public Text time;

    public void Start()
    {
        Instance = this;
        timeRemaining = FindObjectOfType<GameManager>().GetComponent<GameManager>().gameTime;  //Getting the time value assigned in the Game Manager class
        DisplayTime(timeRemaining); //calling method to show time text
    }
    void Update()
    {
        //Check if the Game has started then start the timer
        if (PlayerController.Instance.start == true && timerIsRunning==false)
        {
            timerIsRunning = true;
        }
        if (timerIsRunning)
        {
            //If time is remaining
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                // time.text = timeRemaining.ToString();
                DisplayTime(timeRemaining);
            } 
            //If time is up
            else
            {
                DisplayTime(0.0f);
                time.GetComponent<Text>().color = Color.red;
                GameManager.Instance.TimeUp(); //calling timeup method


            }
        }
            
    }
    //this method displays time in "{0:00}:{1:00}" format
    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //showing time in format
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
