using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SyncFiles();
    
    private const string FileType = ".json";
    private static string SavePath => Application.persistentDataPath + "/Saves/";
    private static string BackUpSavePath => Application.persistentDataPath + "/BackUps/";

    private static int SaveCount;
    public static void SaveData<T>(T data, string fileName)
    {
        if (SaveCount % 5 == 0)
            Save(BackUpSavePath);
        Save(SavePath);
        SaveCount++;

        void Save(string path)
        {
            string dataData = JsonUtility.ToJson(data);
            string filePath = path + fileName + FileType;
            System.IO.File.WriteAllText(filePath, dataData);
        }
    }

    public static T LoadData<T>(string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        bool backUpInNeed = false;
        T dataToReturn;
        
        Load(SavePath);
        if (backUpInNeed)
        {
            Load(BackUpSavePath);
        }
        
        return dataToReturn;

        void Load(string path)
        {
            string filePath = path + fileName + FileType;
            string dataData = System.IO.File.ReadAllText(filePath);
            dataToReturn = JsonUtility.FromJson<T>(dataData);
        }
    }

    public static bool SaveExists(string fileName)
    {
        return (File.Exists(SavePath + fileName + FileType)) ||
               (File.Exists(BackUpSavePath + fileName + FileType));
    }
}