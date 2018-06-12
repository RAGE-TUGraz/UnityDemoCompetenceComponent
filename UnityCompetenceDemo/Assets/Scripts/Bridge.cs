using AssetPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bridge : IBridge, ILog, IDataStorage
{
    //string IDataStoragePath = @"C:\Users\mojaW\git\UnityDemoCompetenceComponent\UnityCompetenceDemo\Assets\RAGE\IDataStorageFolder\";//"./";
    //string IDataStoragePath = @"C:\Users\mojo\git\UnityDemoCompetenceComponent\UnityCompetenceDemo\Assets\RAGE\IDataStorageFolder\";
    string IDataStoragePath = Application.dataPath + @"\RAGE\IDataStorageFolder\";

    #region IDataStorage

    public bool Delete(string fileId)
    {
        if (Exists(fileId))
        {
            string filePath = IDataStoragePath + fileId;
            File.Delete(filePath);
            return true;
        }
        return false;
    }

    public bool Exists(string fileId)
    {
        string filePath = IDataStoragePath + fileId;
        return (File.Exists(filePath));
    }

    public string[] Files()
    {
        throw new NotImplementedException();
    }

    public string Load(string fileId)
    {
        //Debug.Log(Application.dataPath);
        string filePath = IDataStoragePath + fileId;
        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                return (line);
            }
        }
        catch (Exception e)
        {
            Log(Severity.Error, e.Message);
            Log(Severity.Error, "Error by loading the DM! - Maybe you need to change the path: \"" + IDataStoragePath + "\"");
        }

        return (null);
    }

    public void Save(string fileId, string fileData)
    {
        string filePath = IDataStoragePath + fileId;
        using (StreamWriter file = new StreamWriter(filePath))
        {
            file.Write(fileData);
        }
    }

    #endregion IDataStorage
    #region ILog

    public void Log(Severity severity, string msg)
    {
        Debug.Log("BRIDGE:  " + msg);
    }

    #endregion ILog 
}
