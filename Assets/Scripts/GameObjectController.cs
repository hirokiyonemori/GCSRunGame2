using System.Collections.Generic;
using UnityEngine;
using LaneGame.Gates;
using Sirenix.OdinInspector;

public class GameObjectController : MonoBehaviour
{
    // リストに表示するゲームオブジェクト
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<Gate> leftGate = new List<Gate>();

    public List<Gate> rightGate = new List<Gate>();

    public GateLeft leftGateStage;

    public GateRight rightGateStage;

    [TabGroup("GameObject")]
    public List<GameObject> stageList;


    // ゲームオブジェクトの表示・非表示を切り替えるための関数
    public void SetGameObject(int gameObjectNumber)
    {
        if (gameObjectNumber >= 0 && gameObjectNumber < gameObjects.Count)
        {
            gameObjects[gameObjectNumber].SetActive(!gameObjects[gameObjectNumber].activeSelf);
        }
    }

    public void listGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }


    private void Start()
    {
        int stageNo = GameManager.Instance.GetStage();
        SetStage(stageNo);

        // int hardStageNo = Random.Range(0, 8);

        // //While the stage number has been previously selected, select another one at random.
        // while (hardStageNo != stageNo)
        // {
        //     hardStageNo = Random.Range(0, 8);
        // }

        // SetStage(hardStageNo);

        List<int> leftGateValues = leftGateStage.GetList(stageNo);
        if (leftGateValues == null)
        {
            Debug.LogError("Left gate values are not initialized.");
            return;
        }
        ChangeGatesValues(leftGate, leftGateValues);

        List<int> rightGateValues = rightGateStage.GetList(stageNo);
        if (rightGateValues == null)
        {
            Debug.LogError("Right gate values are not initialized.");
            return;
        }
        ChangeGatesValues(rightGate, rightGateValues);

    }
    private void SetStage(int stageNo)
    {
        foreach (var stage in stageList)
        {
            stage.SetActive(false);
        }
        stageList[stageNo].SetActive(true);

        // switch (stageNo)
        // {
        //     case 0:
        //         for (int i = 0; i < listGameObject1.Count; i++)
        //         {
        //             listGameObject(listGameObject1[i]);
        //         }
        //         break;
        //     case 1:
        //         for (int i = 0; i < listGameObject2.Count; i++)
        //         {
        //             listGameObject(listGameObject2[i]);
        //         }
        //         break;
        //     case 2:
        //         for (int i = 0; i < listGameObject3.Count; i++)
        //         {
        //             listGameObject(listGameObject3[i]);
        //         }
        //         break;
        //     case 3:
        //         for (int i = 0; i < listGameObject4.Count; i++)
        //         {
        //             listGameObject(listGameObject4[i]);
        //         }
        //         break;
        //     case 4:
        //         for (int i = 0; i < listGameObject5.Count; i++)
        //         {
        //             listGameObject(listGameObject5[i]);
        //         }
        //         break;

        //     case 5:
        //         for (int i = 0; i < listGameObject6.Count; i++)
        //         {
        //             listGameObject(listGameObject6[i]);
        //         }
        //         break;
        //     case 6:
        //         for (int i = 0; i < listGameObject7.Count; i++)
        //         {
        //             listGameObject(listGameObject7[i]);
        //         }
        //         break;
        //     case 7:
        //         for (int i = 0; i < listGameObject8.Count; i++)
        //         {
        //             listGameObject(listGameObject8[i]);
        //         }
        //         break;
        //     default:
        //         Debug.LogError("Invalid stage number.");
        //         break;
        // }

    }
    private void ChangeGatesValues(List<Gate> gates, List<int> values)
    {
        for (int i = 0; i < gates.Count; i++)
        {
            int value = values[i];
            gates[i].ChangeValue(value);
        }
    }


}
