/*using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{
    [DllImport ( "__Internal" )]
    private static extern void SyncFiles ( );

    [DllImport ( "__Internal" )]
    private static extern void WindowAlert ( string message );

    
    public static void Save ( Data data )
    {
        string dataPath = string.Format ( "{0}/Data.dat", Application.persistentDataPath );
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
        //Stats stats = null;
        string dataPath = string.Format ( "{0}/Stats.dat", Application.persistentDataPath );

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
}*/