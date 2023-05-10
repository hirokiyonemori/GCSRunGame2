using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBtns : MonoBehaviour
{
    //This class  handles each button on the levels panel individually

    public int levelbtnno;
    public GameObject lockImg;

    void Start()
    {
        name = this.gameObject.name;
        if (PlayerPrefs.GetInt("levelno") < levelbtnno  )
        {
            lockImg.SetActive(true);
        }
    }
    //Called on Level Button click on levels panel
    public void Openthislevel()
    {
        if (PlayerPrefs.GetInt("levelno") >= levelbtnno)
        {
             StartCoroutine(LoadSceneAsync(levelbtnno));
        }
    }
    //Loading the scene and breaking the progress into pieces for progress bar
    public IEnumerator LoadSceneAsync(int levelName)
    {
        FindObjectOfType<MainMenu>().loadingPanel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            FindObjectOfType<MainMenu>().loadingFill.fillAmount = progress;
            yield return null;
        }
    }


}
