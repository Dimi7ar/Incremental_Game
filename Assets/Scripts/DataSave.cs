using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*public class DataSave
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
}*/
public static class DataSave
{
    [DllImport("__Internal")]
    private static extern void JS_FileSystem_Sync();
 
    private static int SaveCount;
    public static void SaveData<T>(T data)
    {
        Directory.CreateDirectory("/idbfs/PrestigeLayersPlayerData");
       
        Save();
 
        void Save()
        {
            using (StreamWriter writer = new StreamWriter("/idbfs/PrestigeLayersPlayerData/data.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, data);
                string dataToSave = Convert.ToBase64String(memoryStream.ToArray());
                writer.WriteLine(dataToSave);
                JS_FileSystem_Sync();
            }
        }
    }
 
    public static T LoadData<T>()
    {
        Directory.CreateDirectory("/idbfs/PrestigeLayersPlayerData");
 
        T dataToReturn;
       
        Load();
       
        return dataToReturn;
 
        void Load()
        {
            using (StreamReader reader = new StreamReader("/idbfs/PrestigeLayersPlayerData/data.dat"))
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
                    dataToReturn = default;
                }
            }
        }
    }
 
    public static bool SaveExists()
    {
        return File.Exists("/idbfs/PrestigeLayersPlayerData/data.dat");
    }
}