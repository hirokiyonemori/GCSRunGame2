using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //This Class deals with the Main Menu
    public GameObject loadingPanel;
    public GameObject levelsPanel;
    public GameObject buttons;
    
    public Image loadingFill;
    public Sprite audioOn;
    public Sprite audioOff;
    public Button audiobtn;
    
    public int maxLevels;
    private int levelNo;
    private int audioVal;

    void Awake()
    {
        //Setting vales on Scene start
        if (!PlayerPrefs.HasKey("levelno"))
        {
            PlayerPrefs.SetInt("levelno", 1);
        }
        if (!PlayerPrefs.HasKey("audio"))
        {
            PlayerPrefs.SetInt("audio", 1);
        }
        if (PlayerPrefs.GetInt("audio") == 1)
        {
            AudioListener.pause = false;
        }
        else if (PlayerPrefs.GetInt("audio") == 0)
        {
            AudioListener.pause = true;
            audiobtn.GetComponent<Image>().sprite = audioOff;
        }
       
    }

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("background");
        PlayerPrefs.SetInt("maxlevels", maxLevels);
        levelNo = PlayerPrefs.GetInt("levelno");
        audioVal = PlayerPrefs.GetInt("audio");
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Quit the application
                Application.Quit();
            }
        }
    }

    //Play the Last unlocked level
    public void Play()
    {
        loadingPanel.SetActive(true);
        buttons.SetActive(false);
        StartCoroutine(LoadSceneAsync(levelNo));
    }

    //Loading the scene and breaking the progress into pieces for progress bar
    public IEnumerator LoadSceneAsync(int levelName)
    {
        loadingPanel.SetActive(true);
        if(levelName> maxLevels)
        {
            levelName = maxLevels;
        }
        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            loadingFill.fillAmount = progress;
            yield return null;
        }
    }

    
    public void OpenLevelPanel()
    {
        levelsPanel.SetActive(true);
        buttons.SetActive(false);
    }
    public void CloseLevelPanel()
    {
        levelsPanel.SetActive(false);
        buttons.SetActive(true);
    } 
    public void ExitGame()
    {
        Application.Quit();
    }

    //Turn Audio on and off
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

}
