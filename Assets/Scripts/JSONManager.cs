using UnityEngine;
using System.IO;

public static class JSONManager
{
    public static string fileName;

    public static JSONPatient Load()
    {
        string fullPath = Application.persistentDataPath + fileName;
        JSONPatient patient = new JSONPatient();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            patient = JsonUtility.FromJson<JSONPatient>(json);
        }
        else
        {
            Debug.Log("JSON file not found at: "+ fullPath);
        }

        return patient;
    }
}
