using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/saves/";
    public static readonly string FILE_EXT = ".json";

    public static void Initialize()
    {
        Debug.Log(Application.dataPath);
        string savesFolderPath = Application.dataPath + "/saves/";

        if (!Directory.Exists(savesFolderPath))
        {
            Directory.CreateDirectory(savesFolderPath);
        }

        string saveFilePath = savesFolderPath + "save.json";
        if (!File.Exists(saveFilePath))
        {
            string jsonData = "{\"highscore\": 0}";
            File.WriteAllText(saveFilePath, jsonData);
        }

        Debug.Log("Initialized");
    }

    public static void Save(string filename, string data)
    {
        try
        {
            string filePath = SAVE_FOLDER + filename + FILE_EXT;
            Debug.Log("Saving file: " + filePath);

            File.WriteAllText(filePath, data);
            Debug.Log("File saved successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }


    public static string Load(string filename)
    {
        string fileLocation = SAVE_FOLDER + filename + FILE_EXT;
        if (File.Exists(fileLocation))
        {
            string loadedData = File.ReadAllText(fileLocation);
            Debug.Log("Load");
            return loadedData;
        }
        else
        {
            return null;
        }
    }
}
