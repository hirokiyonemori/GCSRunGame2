using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
//using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public RectTransform tutorialTextArea;
    public TextMeshProUGUI TutorialTitle;
    public TextMeshProUGUI TutorialText;

    // ?`???[?g???A???^?X?N
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    // ?`???[?g???A???\???t???O
    //private bool isEnabled;

    // ?`???[?g???A???^?X?N????????????????J??p?t???O
    private bool task_executed = false;

    // ?`???[?g???A???\??????UI???????
    private float fade_pos_x = 460;

    private float fade_pos_y = 90;

    [SerializeField]
    private bool tutorialFlag;


    void Start()
    {
        // 解像度で移動距離を変更する
        fade_pos_x = 460 * Screen.width / 480;
        fade_pos_y = 90 * Screen.height / 800;



        //tutorialFlag = ES3.Load<bool>("Tutorial");
        //IntKeyというKeyで保存されたデータが存在してるか
        bool existsKey = ES3.KeyExists("Tutorial");
		if (existsKey)
		{
            Debug.Log(" test 2");
            
		}
		else
		{
            Debug.Log(" test ");
            ES3.Save<bool>("Tutorial", false);
        }

        existsKey = false;

        if (tutorialFlag)
        {
            tutorialTextArea.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }


        // ?`???[?g???A?????
        tutorialTask = new List<ITutorialTask>()
        {
            new MovementTask(),
            //new JumpTask(),
            new EndTask(),

        };

        // ?????`???[?g???A??????
        StartCoroutine(SetCurrentTask(tutorialTask.First()));
    }

    void Update()
    {
        // ?`???[?g???A????????????s????????????????
        if (currentTask != null && !task_executed)
        {
            // ?????`???[?g???A???????s??????????
            if (currentTask.CheckTask())
            {
                task_executed = true;

                //DOVirtual.DelayedCall(currentTask.GetTransitionTime(), () => {
                //iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
                //"position", tutorialTextArea.transform.position + new Vector3(fade_pos_x, 0, 0),
                //"time", 1f
                //));

                //    });
                tutorialTask.RemoveAt(0);

                var nextTask = tutorialTask.FirstOrDefault();
                if (nextTask != null)
                {
                    StartCoroutine(SetCurrentTask(nextTask, 1f));
                }
                else
                {
                    tutorialFlag = true;
                }
            }
        }

    }

    public bool CheckTutorialEnd()
    {
        return tutorialFlag;
    }

    /// <summary>
    /// ?V?????`???[?g???A???^?X?N??????
    /// </summary>
    /// <param name="task"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    protected IEnumerator SetCurrentTask(ITutorialTask task, float time = 0)
    {
        // time???w????????????@
        yield return new WaitForSeconds(time);

        currentTask = task;
        task_executed = false;

        // UI??^?C?g?????????????
        TutorialTitle.text = task.GetTitle();
        //Debug.Log(" task.GetTitle()" + task.GetTitle());
        TutorialText.text = task.GetText();

//        Debug.Log(" task.GetText()" + task.GetText());

        // ?`???[?g???A???^?X?N??莞?p?????????s
        task.OnTaskSetting();

        //iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
            //"position", tutorialTextArea.transform.position - new Vector3(fade_pos_x, 0, 0),
            //"time", 1f
        //));
    }

    
}
