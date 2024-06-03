using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; /* StreamWriter */
using System; /* DateTime */

public class LogManager : ManagerBase
{
    
    static string logFilePath = Path.Combine(Application.persistentDataPath, "log.txt");

    public override void Init() {
        File.WriteAllText(logFilePath, "Log started at: " + System.DateTime.Now + "\n");
        Application.logMessageReceived += saveLog;
    }

    public void saveLog(string logString, string stackTrace, LogType type)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine("[" + type + "] " + logString);
            writer.WriteLine(stackTrace);

        }
       
    }
    public void Clear()
    {

        
    }
    public void OnDestroy()
    {
        Application.logMessageReceived -= saveLog;
    }
}
