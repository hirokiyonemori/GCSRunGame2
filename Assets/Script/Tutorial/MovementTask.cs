using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Localization.Settings;

public class MovementTask : ITutorialTask
{
    string targetTableName = "StringTable";

    public string GetTitle()
    {
        // Get
        //var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        //var value = table.GetEntry("TutorialTitle1").Value;
        //Debug.Log(" value " + value);


        //var value2 = table.GetEntry("TutorialContents1").Value;

        //Debug.Log(" value2 " + value2);
        var value = "�ړ����悤";
        var value2 = "�ړ����悤";
        return value + "\n" + value2;
    }

    public string GetText()
    {
        // Get
        //var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        //var value = table.GetEntry("TutorialContents1").Value;
        var value = "�ړ����悤";
        //var value2 = "�ړ����悤";
        Debug.Log(" value " + value);
        return "";
    }

    public void OnTaskSetting()
    {
        // ����O�̍��W��ݒ肷��
        previousPos = currentPos;
    }

    const float LOAD_WIDTH = 6f;

    Vector3 previousPos, currentPos;

    public bool CheckTask()
    {
        if (Input.GetMouseButton(0))
        {
            // �X���C�v�ɂ��ړ��������擾
            currentPos = Input.mousePosition;

        }
        // �ړ��������ꍇ
        if (previousPos.x != currentPos.x)
        {
            return true;
        }

        return false;
    }

    public float GetTransitionTime()
    {
        return 2f;
    }
}
