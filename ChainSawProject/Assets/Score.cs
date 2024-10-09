using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Score : MonoSingleton<Score>
{
    public int DieCount;

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/diecount.json"; // Set the file path for JSON storage
        LoadDieCount(); // Load the die count when the game starts
    }

    // Call this function whenever an enemy dies to increment the count and save
    public void IncrementDieCount()
    {
        DieCount++;
        SaveDieCount(); // Save the updated DieCount to JSON
    }

    // Save the die count to a JSON file
    public void SaveDieCount()
    {
        SaveData data = new SaveData { DieCount = this.DieCount }; // Create a SaveData object
        string json = JsonUtility.ToJson(data, true); // Convert the object to JSON format
        File.WriteAllText(filePath, json); // Write the JSON to a file
        Debug.Log("DieCount saved to JSON: " + DieCount);
    }

    // Load the die count from the JSON file
    public void LoadDieCount()
    {
        if (File.Exists(filePath)) // Check if the file exists
        {
            string json = File.ReadAllText(filePath); // Read the JSON from the file
            SaveData data = JsonUtility.FromJson<SaveData>(json); // Convert JSON back to an object
            DieCount = data.DieCount; // Set the DieCount from the loaded data
            Debug.Log("DieCount loaded from JSON: " + DieCount);
        }
        else
        {
            DieCount = 0; // If no file exists, start with 0
            Debug.Log("No save file found, setting DieCount to 0.");
        }
    }

    // Class to store die count in JSON format
    [System.Serializable]
    private class SaveData
    {
        public int DieCount;
    }
}
