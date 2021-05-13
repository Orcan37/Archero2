using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebugScriptForAndroid : MonoBehaviour
{
    public void Log()
    {
        Debug.Log("Text Log");
    }
    public   void LogError()
    {
        Debug.LogError("Text ErrorLog");
    }
   
    public void LogWarning()
    {
        Debug.LogWarning("Text Warning");
    }
}
