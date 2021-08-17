using UnityEngine;
using System.IO;


public static class CSVManager
{
    private static string reportDirectoryName = "Report";
    private static string reportFileName = "Report1.csv";
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[18] {
        "Trial #",/*
        "Participant #",
        "Age",
        "Sex",
        "Handedness",*/
        "Type Triangle",
        "Left or Right", 
        "Starting Pole Location",
        "Second Pole Location",
        "Third Pole Location",
        "Correct Distance",
        "Input Distance",
        "Correct Angle",
        "Input Angle",
        "Angular Error",
        "Percent Angular 180 Error",
        /*"Total Trial Time (Updating)",
        "Total Trial Time (Final)",
        "Trial Time During First Leg",
        "Trial Time During First Turn",
        "Trial Time During Second Leg",
        "Trial Time During Second Turn", 
        "Trial Time During Third Leg",*/
        "Location X",
        "Location Y",
        "Location Z",
        "Rotation X",
        "Rotation Y",
        "Rotation Z"
    };
    private static string timeStampHeader = "Time Stamp";
    #region Interactions
    public static void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += strings[i];
            }
            finalString += reportSeparator + GetTimeStamp();
            sw.WriteLine(finalString);
        }
    }
    public static void CreateReport()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += reportHeaders[i];
            }
            finalString += reportSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }
    #endregion
    #region Operations
    static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }
    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }
    #endregion
    #region Queries
    static string GetDirectoryPath()
    {
        return Application.dataPath + "/" + reportDirectoryName;
    }
    static string GetFilePath()
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }
    static string GetTimeStamp()
    {
        return System.DateTime.Now.ToString();
    }
    #endregion
}

