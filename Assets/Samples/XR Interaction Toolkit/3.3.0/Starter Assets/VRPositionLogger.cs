using UnityEngine;
using System.IO;
using System.Text;

public class VRPositionLogger : MonoBehaviour
{
    [Header("Settings")]
    public string conditionName = "Magnetic"; // Change this in Inspector for each test
    public float logInterval = 0.5f; // How often to record (seconds)
    
    [Header("Target")]
    public Transform playerTransform; // Drag the Main Camera or Player Root here

    private string filePath;
    private float timer;
    private StringBuilder csvContent = new StringBuilder();

    public float participantNumber = 1;

    void Start()
    {
        // 1. Set the file path (saves to: C:\Users\Name\AppData\LocalLow\YourCompany\YourProject)
        string folderPath = Application.persistentDataPath + "/TrackingData/";
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        // Name file with timestamp to avoid overwriting previous tests
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
        filePath = folderPath + conditionName + "_" +  timestamp + "_Participant" + participantNumber.ToString() + ".csv";

        // 2. Write CSV Header
        string header = "Time,PosX,PosY,PosZ,RotX,RotY,RotZ\n";
        File.WriteAllText(filePath, header);

        Debug.Log($"Logging started! Saving to: {filePath}");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= logInterval)
        {
            RecordData();
            timer = 0;
        }
    }

    void RecordData()
    {
        if (playerTransform == null) return;

        // Collect Position and Rotation
        Vector3 pos = playerTransform.position;
        Vector3 rot = playerTransform.eulerAngles;

        // Create CSV line: Time, X, Y, Z, RotX, RotY, RotZ
        string line = string.Format("{0:F2},{1:F3},{2:F3},{3:F3},{4:F1},{5:F1},{6:F1}\n",
            Time.time, pos.x, pos.y, pos.z, rot.x, rot.y, rot.z);

        // Append to file
        File.AppendAllText(filePath, line);
    }
}