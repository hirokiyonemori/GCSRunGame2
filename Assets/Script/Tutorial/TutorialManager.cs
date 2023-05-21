using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    public RectTransform tutorialTextArea;
    public TextMeshProUGUI TutorialTitle;
    public TextMeshProUGUI TutorialText;

    // ?`???[?g???A???^?X?N
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    public GameObject StartGameUI;

    // ?`???[?g???A???\???t???O
    //private bool isEnabled;

    // ?`???[?g???A???^?X?N????????????????J??p?t???O
    private bool task_executed = false;

    // ?`???[?g???A???\??????UI???????
    private float fade_pos_x = 460;

    private float fade_pos_y = 90;

    [SerializeField]
    private bool tutorialFlag;

    [SerializeField]
    private List<GameObject> objList;
    void Start()
    {
        // �𑜓x�ňړ�������ύX����


        //tutorialFlag = ES3.Load<bool>("Tutorial");
        //IntKey�Ƃ���Key�ŕۑ����ꂽ�f�[�^�����݂��Ă邩
        bool existsKey = ES3.KeyExists("Tutorial");
        int tutorialNo = 0;
        if (existsKey)
        {
            tutorialNo = ES3.Load<int>("Tutorial");
        }
        else
        {
            ES3.Save<int>("Tutorial", 0);
        }

        if (tutorialNo >= 100)
        {
            StartGameUI.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        else
        {
            StartGameUI.SetActive(false);
        }

        if (tutorialFlag)
        {
            //tutorialTextArea.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }

        //        DOTween.Init();
        // ?`???[?g???A?????
        // tutorialTask = new List<ITutorialTask>()
        // {
        //     new MovementTask(),
        //     //new JumpTask(),
        //     new PeopleMoveTask(),
        //     new ObstacleTask(),
        //     new EndTask(),

        // };
        // // ?????`???[?g???A??????
        // StartCoroutine(SetCurrentTask(tutorialTask.First()));
    }

    int taskCnt = 0;
    public void NextTask()
    {
        objList[taskCnt].SetActive(false);
        taskCnt++;
        if (taskCnt < objList.Count)
        {
            objList[taskCnt].SetActive(true);
        }
        else
        {
            StartGameUI.SetActive(true);
            gameObject.SetActive(false);
            tutorialFlag = true;
            ES3.Save<int>("Tutorial", 100);
        }
    }

    public void BackTask()
    {
        objList[taskCnt].SetActive(false);
        taskCnt--;
        if (taskCnt >= 0)
        {
            objList[taskCnt].SetActive(true);
        }
    }

    // public void Check()
    // {
    //     // ?`???[?g???A????????????s????????????????
    //     if (currentTask != null && !task_executed)
    //     {
    //         // ?????`???[?g???A???????s??????????
    //         if (currentTask.CheckTask())
    //         {
    //             task_executed = true;

    //             // DOVirtual.DelayedCall(currentTask.GetTransitionTime(), () =>
    //             // {
    //             //     iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
    //             //     "position", tutorialTextArea.transform.position + new Vector3(fade_pos_x, 0, 0),
    //             //     "time", 1f
    //             //     ));
    //             // }

    //             //    });
    //             tutorialTask.RemoveAt(0);

    //             var nextTask = tutorialTask.FirstOrDefault();
    //             if (nextTask != null)
    //             {
    //                 StartCoroutine(SetCurrentTask(nextTask, 1f));
    //             }
    //             else
    //             {
    //                 StartGameUI.SetActive(true);
    //                 gameObject.SetActive(false);
    //                 tutorialFlag = true;
    //             }
    //         }
    //     }

    // }

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

        // ?`???[?g???A???^?X?N??��?p?????????s
        task.OnTaskSetting();

        //iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
        //"position", tutorialTextArea.transform.position - new Vector3(fade_pos_x, 0, 0),
        //"time", 1f
        //));
    }


}
