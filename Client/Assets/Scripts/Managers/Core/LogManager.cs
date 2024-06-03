using UnityEngine;
using System.IO;

public class LogManager : ManagerBase
{
    private string logFilePath;

    public override void Init()
    {
        // 로그 파일 경로 설정
        logFilePath = Application.persistentDataPath + "/log.txt";

        // 로그 파일 초기화 (기존 내용 지우기)
        File.WriteAllText(logFilePath, "Log started at: " + System.DateTime.Now + "\n");

        // Debug.Log에 커스텀 로깅 메서드 등록
        Application.logMessageReceived += LogToFile;
    }

    void LogToFile(string logString, string stackTrace, LogType type)
    {
        try
        {
            // 로그 메시지를 파일에 기록
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("[" + type + "] " + logString);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Failed to write to log file: " + e.Message);
        }
    }

    void OnDestroy()
    {
        // Debug.Log에서 커스텀 로깅 메서드 해제
        Application.logMessageReceived -= LogToFile;
    }

}
