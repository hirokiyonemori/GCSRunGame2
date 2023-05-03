using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSystem 
{
    public static bool isdebug;
    public static void Log(string log)
    {
        if (isdebug)
        {
            Debug.Log(log);
        }
    }


    public static void LogWarning(string log)
    {
        if (isdebug)
        {
            Debug.LogWarning(log);
        }
    }
}
