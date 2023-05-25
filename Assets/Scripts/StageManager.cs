using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // <summary>
    /// 廃止しています。
    /// </summary>
    // ステージごとの処理を格納する辞書
    private Dictionary<int, string> stageActions = new Dictionary<int, string>();

    // CSVファイルからステージ情報を読み込む
    // Resources.Load()メソッドを使用してCSVファイルを読み込みます。
    private void LoadStageData()
    {
        TextAsset csvFile = Resources.Load("stage_data") as TextAsset;
        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            int stageNumber = int.Parse(data[0]);
            string action = data[1].Trim();
            stageActions.Add(stageNumber, action);
        }
    }
    // <summary>
    /// 廃止しています。
    /// </summary>
    // ステージ番号に応じて処理を実行する
    public void RunStage(int stageNumber)
    {
        if (stageActions.ContainsKey(stageNumber))
        {
            Debug.Log($"Running stage {stageNumber} action: {stageActions[stageNumber]}");
            // ここに対応する処理を記述する
        }
        else
        {
            Debug.LogWarning($"No action found for stage {stageNumber}");
        }
    }


    public int unlockedStages; //現在アンロックされたステージの数
    public List<Button> stageButtons; //各ステージを表すボタンのリスト
    public static int STAGE_MAX = 8;
    private void Start()
    {
        //保存されている情報からアンロックされたステージ数を復元
        unlockedStages = GameManager.Instance.StageClear + 1;

        //全てのステージボタンを非アクティブにする
        foreach (Button button in stageButtons)
        {
            button.interactable = false;
        }

        //アンロックされた数に応じてステージボタンをアクティブにする
        for (int i = 0; i < unlockedStages; i++)
        {
            //stageButtons[i].SetActive(true);
            if (stageButtons.Count > i)
            {
                stageButtons[i].interactable = true;
            }
        }
    }

    //新しいステージがクリアされたときに呼び出されるメソッド
    public void UnlockStage()
    {
        //unlockedStages++; //アンロックされたステージ数を増加させる

        //アンロック状態を保存する
        //PlayerPrefs.SetInt("UnlockedStages", unlockedStages);

        //最新のアンロック状態に応じてステージボタンをアクティブにする
        for (int i = 0; i < unlockedStages; i++)
        {
            stageButtons[i].interactable = true;
        }
    }
}
