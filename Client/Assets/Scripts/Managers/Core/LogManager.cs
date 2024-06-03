using UnityEngine;
using System.IO;

public class LogManager : ManagerBase
{
    private string logFilePath;

    public override void Init()
    {
        // �α� ���� ��� ����
        logFilePath = Application.persistentDataPath + "/log.txt";

        // �α� ���� �ʱ�ȭ (���� ���� �����)
        File.WriteAllText(logFilePath, "Log started at: " + System.DateTime.Now + "\n");

        // Debug.Log�� Ŀ���� �α� �޼��� ���
        Application.logMessageReceived += LogToFile;
    }

    void LogToFile(string logString, string stackTrace, LogType type)
    {
        try
        {
            // �α� �޽����� ���Ͽ� ���
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
        // Debug.Log���� Ŀ���� �α� �޼��� ����
        Application.logMessageReceived -= LogToFile;
    }

}
