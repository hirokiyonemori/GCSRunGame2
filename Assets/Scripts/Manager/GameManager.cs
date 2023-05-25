using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // インスタンス
    private static GameManager instance;

    // プロパティ
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

    }


    private bool hardMode = false;

    public bool HardMode
    {
        get { return hardMode; }
        set { hardMode = value; }
    }

    private int playerStartUnit;

    public int PlayerStartUnit
    {
        get { return playerStartUnit; }
        set { playerStartUnit = value; }
    }

    private int stageClear;

    public int StageClear
    {
        get { return stageClear; }
        set { stageClear = value; }
    }
    private int playCnt = 0;
    public int PlayCnt
    {
        get { return playCnt; }
        set { playCnt = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンを切り替えても破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既に存在するインスタンスがある場合は、自分自身を破棄する
        }
        if (ES3.KeyExists("PlayerUnit"))
        {
            playerStartUnit = ES3.Load<int>("PlayerUnit");
        }
        else
        {
            playerStartUnit = 0;
            ES3.Save("PlayerUnit", playerStartUnit);
        }
        if (ES3.KeyExists("HardMode"))
        {
            //hardMode = ES3.Load<bool>("HardMode");
        }
        else
        {
            //hardMode = false;
            ES3.Save("HardMode", false);
        }
        if (ES3.KeyExists("StageClear"))
        {
            stageClear = ES3.Load<int>("StageClear");
        }
        else
        {

            stageClear = 0;
            ES3.Save("StageClear", stageClear);
        }
        PlayCnt = 0;


    }

    // 変数
    private int stageNo;

    // シーン間でデータを共有するためのメソッド
    public void SetStage(int no)
    {
        this.stageNo = no;
    }

    // 
    public int GetStage()
    {
        return this.stageNo;
    }
    public int GetPlayerStartUnit()
    {
        return playerStartUnit;
    }

    public void SetPlayerStartUnit(int playerStartUnit)
    {
        this.playerStartUnit = playerStartUnit;
        ES3.Save("PlayerUnit", playerStartUnit);

    }
}
