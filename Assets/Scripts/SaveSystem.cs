using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/saves/"; // Путь к папке сохранений
    public static readonly string FILE_EXT = ".json"; // Расширение файлов сохранений

    public static void Initialize()
    {
        Debug.Log(Application.dataPath); // Выводим путь к папке с данными приложения
        string savesFolderPath = Application.dataPath + "/saves/";

        if (!Directory.Exists(savesFolderPath))
        {
            Directory.CreateDirectory(savesFolderPath); // Создаем папку сохранений, если она не существует
        }

        string saveFilePath = savesFolderPath + "save.json";
        if (!File.Exists(saveFilePath))
        {
            string jsonData = "{\"highscore\": 0}"; // Создаем файл сохранения по умолчанию с начальными данными
            File.WriteAllText(saveFilePath, jsonData); // Записываем данные в файл
        }

        Debug.Log("Initialized");
    }

    public static void Save(string filename, string data)
    {
        try
        {
            string filePath = SAVE_FOLDER + filename + FILE_EXT; // Формируем путь к файлу сохранения
            Debug.Log("Saving file: " + filePath);

            File.WriteAllText(filePath, data); // Записываем данные в файл
            Debug.Log("File saved successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }

    public static string Load(string filename)
    {
        string fileLocation = SAVE_FOLDER + filename + FILE_EXT; // Формируем путь к файлу сохранения
        if (File.Exists(fileLocation))
        {
            string loadedData = File.ReadAllText(fileLocation); // Считываем данные из файла
            Debug.Log("Load");
            return loadedData;
        }
        else
        {
            return null;
        }
    }
}
