using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //This class manages all the main components of Level

    public static GameManager Instance;

    [HideInInspector]
    public bool firstTime;
    public bool gameOver=false;
    public bool timeup=false;
    public bool levelCompleted = false;
    private bool playTimeuP = false;

    private int totalLevels;
    private int audioVal;
    public int levelNo;
    public float gameTime; //Time left
    private float timeRemaining = 7.0f;

    public GameObject gameCompletedPanel;
    public GameObject gameOverPanel;
    public GameObject gameTimeUpPanel;
    public GameObject swipeUpText;
    public GameObject swipeUpArrow;
    public GameObject HoldImageBtn;
    public GameObject taptoStartText;

    public Sprite audioOn;
    public Sprite audioOff;
    public Button audiobtn;
    public Text leveltext;

    private void Awake()
    {
        //Setting audio on start of scene
        if (PlayerPrefs.GetInt("audio") == 1)
        {
            AudioListener.pause = false;
        } else if (PlayerPrefs.GetInt("audio") == 0)
        {
            AudioListener.pause = true;
            audiobtn.GetComponent<Image>().sprite = audioOff;
        }
        //Setting Ad Status on start of scene
        if (!PlayerPrefs.HasKey("loosecount"))
        {
            PlayerPrefs.SetInt("loosecount", 1);

        }

    }
    void Start()
    {
        Instance = this;

        //Checking if game is being played first time or not
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime", 0);
        }
        if(PlayerPrefs.GetInt("firstTime") == 1)
        {
            taptoStartText.SetActive(true);
        }

        //Getting saved ales from player prefs
        audioVal = PlayerPrefs.GetInt("audio");
        totalLevels = PlayerPrefs.GetInt("maxlevels");

        leveltext.text = levelNo.ToString();
    }

    //Turn audio on or off
    public void AudioToggle()
    {
        if (audioVal == 0)
        {
            PlayerPrefs.SetInt("audio", 1);
            audioVal = 1;
            audiobtn.GetComponent<Image>().sprite = audioOn;

            AudioListener.pause = false;
        }
        else if (audioVal == 1)
        {
            PlayerPrefs.SetInt("audio", 0);
            audioVal = 0;
            audiobtn.GetComponent<Image>().sprite = audioOff;
            AudioListener.pause = true;


        }
    }
    // Update is called once per frame
    void Update()
    {
        //If game is played first time show instructions panel
        if (PlayerPrefs.GetInt("firstTime") == 0)
        {
          FirstTime();
        }

        if (gameOver)
        {
            PlayerController.Instance.start = false;
             CountDownTimeLeft.Instance.timerIsRunning = false;
             Invoke("GameOverLate", 2f);
        }
        else if (timeup)
        {
            FindObjectOfType<AudioManager>().Stop("background");
            Invoke("GameOverLate", 2f);
        }
        else if (levelCompleted)
        {
            PlayerController.Instance.start = false;
            CountDownTimeLeft.Instance.timerIsRunning = false;
            Invoke("GameOverLate", 3f);
        }
        //if platform is android
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                Back();
            }
        }
    }
    //If Game time is up
    public void TimeUp()
    {
        PlayerController.Instance.run = false;
        PlayerController.Instance.anim.SetTrigger("idle");
        PlayerController.Instance.gameOver = true;
        PlayerWPFollowScript.Instance.run = false;
        
        timeup = true;
        Invoke("GameOverLate", 2f);

        if (playTimeuP == false)

        {
            FindObjectOfType<AudioManager>().Play("timesup");
            playTimeuP = true;
        }
    }

    //Call this method after sometime when game is over
    public void GameOverLate()
    {
        Time.timeScale = 0;
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
        }
        else if (timeup)
        {
            gameTimeUpPanel.SetActive(true);
        }
        else if (levelCompleted)
        {
            gameCompletedPanel.SetActive(true);
            if (PlayerPrefs.GetInt("levelno") < PlayerPrefs.GetInt("maxlevels"))
            {
                PlayerPrefs.SetInt("levelno", PlayerPrefs.GetInt("levelno") + 1);
            }
        }
    }

    //If game is played first time show instructions panel
    public void FirstTime()
    {
       
        if (timeRemaining > 4)
        {
          
            timeRemaining -= Time.deltaTime;
            HoldImageBtn.SetActive(true);
        }
        else if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            HoldImageBtn.SetActive(false);
            swipeUpArrow.SetActive(true);
            swipeUpText.SetActive(true);
        }
        else if (timeRemaining <= 0)
        {
            swipeUpArrow.SetActive(false);
            swipeUpText.SetActive(false);
            firstTime = false;
            PlayerPrefs.SetInt("firstTime", 1);
            taptoStartText.SetActive(true);
           
        }
    }
    //When back button is pressed call this method
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    //Replay the same level, called on replay button click
    public void ReplayGame()
    {
        if (PlayerPrefs.GetInt("loosecount") / 2 == 0.0f)
        {

            PlayerPrefs.SetInt("loosecount",PlayerPrefs.GetInt("loosecount") + 1);
            if (PlayerPrefs.GetInt("loosecount") > 3)
            {
                PlayerPrefs.SetInt("loosecount", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("loosecount", PlayerPrefs.GetInt("loosecount") + 1);
            if (PlayerPrefs.GetInt("loosecount") > 3)
            {
                PlayerPrefs.SetInt("loosecount", 1);
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 

    //Open next level
    public void NextLevel()
    {
        int levelno= PlayerPrefs.GetInt("levelno");
        if (levelno > totalLevels)
        {
            levelno = totalLevels;
        }
        SceneManager.LoadScene(levelno);
    }
}
