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
/*using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSave
{
    [DllImport ( "__Internal" )]
    private static extern void SyncFiles ( );

    [DllImport ( "__Internal" )]
    private static extern void WindowAlert ( string message );

    public static void Save ( Data data )
    {
        string dataPath = string.Format ( "/idbfs/incremental_game_player_data/data.dat" );
        BinaryFormatter binaryFormatter = new BinaryFormatter ( );
        FileStream fileStream;

        try
        {
            if ( File.Exists ( dataPath ) )
            {
                File.WriteAllText ( dataPath, string.Empty );
                fileStream = File.Open ( dataPath, FileMode.Open );
            }
            else
            {
                fileStream = File.Create ( dataPath );
            }

            binaryFormatter.Serialize ( fileStream, data );
            fileStream.Close ( );

            if ( Application.platform == RuntimePlatform.WebGLPlayer )
            {
                SyncFiles ( );
            }
        }
        catch ( Exception e )
        {
            PlatformSafeMessage ( "Failed to Save: " + e.Message );
        }
    }

    public static Data Load (Data data )
    {
        string dataPath = string.Format ( "/idbfs/incremental_game_player_data/data.dat" );

        try
        {
            if ( File.Exists ( dataPath ) )
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter ( );
                FileStream fileStream = File.Open ( dataPath, FileMode.Open );

                data = ( Data ) binaryFormatter.Deserialize ( fileStream );
                fileStream.Close ( );
            }
        }
        catch ( Exception e )
        {
            PlatformSafeMessage ( "Failed to Load: " + e.Message );
        }

        return data;
    }

    private static void PlatformSafeMessage ( string message )
    {
        if ( Application.platform == RuntimePlatform.WebGLPlayer )
        {
            WindowAlert ( message );
        }
        else
        {
            Debug.Log ( message );
        }
    }
    
    public static bool SaveExists()
    {
        return File.Exists("/idbfs/incremental_game_player_data/data.dat");
    }
}*/
