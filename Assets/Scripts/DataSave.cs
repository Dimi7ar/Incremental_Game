using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSave
{
    private const string FileType = ".txt";

    private static int SaveCount;
    public static void SaveData<T>(T data, string fileName)
    {
        Save();

        void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, data);
            string dataToSave = Convert.ToBase64String(memoryStream.ToArray());
            PlayerPrefs.SetString(fileName + FileType, dataToSave);
        }
    }

    public static T LoadData<T>(string fileName)
    {
        T dataToReturn = default;

        Load();

        return dataToReturn;

        void Load()
        {
            string dataToLoad = PlayerPrefs.GetString(fileName + FileType, "");
            if (!string.IsNullOrEmpty(dataToLoad))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(dataToLoad));
                try
                {
                    dataToReturn = (T)formatter.Deserialize(memoryStream);
                }
                catch
                {
                    dataToReturn = default;
                }
            }
            else
            {
                dataToReturn = default;
            }
        }
    }

    public static bool SaveExists(string fileName)
    {
        return PlayerPrefs.HasKey(fileName + FileType);
    }
}
/*public static class DataSave
{
    [DllImport("__Internal")]
    private static extern void JS_FileSystem_Sync();
    private static string SavePath => Application.persistentDataPath + "/saves/";
    private static string BackUpSavePath => Application.persistentDataPath + "/backups/";
 
    private static int SaveCount;
    public static void SaveData<T>(T data, string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);
       
        if (SaveCount % 5 == 0)
            Save(BackUpSavePath);
        Save(SavePath);
        SaveCount++;
 
        void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path + fileName + FileType))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, data);
                string dataToSave = Convert.ToBase64String(memoryStream.ToArray());
                writer.WriteLine(dataToSave);
            }
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
            using (StreamReader reader = new StreamReader(path + fileName + FileType))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string dataToLoad = reader.ReadToEnd();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(dataToLoad));
                try
                {
                    dataToReturn = (T)formatter.Deserialize(memoryStream);
                }
                catch
                {
                    backUpInNeed = true;
                    dataToReturn = default;
                }
            }
        }
    }
 
    public static bool SaveExists(string fileName)
    {
        return (File.Exists(SavePath + fileName + FileType)) ||
               (File.Exists(BackUpSavePath + fileName + FileType));
    }
}*/