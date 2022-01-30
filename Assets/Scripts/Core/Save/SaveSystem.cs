using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public static class SaveSystem
{
    private static readonly string saveFolder = Application.persistentDataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    public static void Save(string saveString)
    {

        var saveFiles = GetAllSaveFilesInfo();

        var oldestFile = GetOldestFileInfo(saveFiles);

        if (saveFiles.Length > 4)
        {
            File.Delete(oldestFile.FullName);
        }

        int saveNumber = 1;

        while (File.Exists(saveFolder + "save_" + saveNumber + ".json"))
        {
            saveNumber++;
        }
        File.WriteAllText(saveFolder + "save_" + saveNumber + ".json", saveString);
    }

    public static string Load()
    {
        var saveFiles = GetAllSaveFilesInfo();

        var mostRecentFile = GetMostRecentFileInfo(saveFiles);

        if (mostRecentFile != null)
        {
            var saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string Load(string fileName)
    {
        var saveFiles = GetAllSaveFilesInfo();

        var fileInfo = saveFiles.ToList().Find(file => fileName.Equals(file.Name));
        var saveString = File.ReadAllText(fileInfo.FullName);

        return saveString;
    }

    private static FileInfo[] GetAllSaveFilesInfo()
    {
        var directoryInfo = new DirectoryInfo(saveFolder);
        Regex reg = new Regex(@"save_[0-9]+.json$");

        var saveFiles = directoryInfo.GetFiles()
            .Where(fileInfo => reg.IsMatch(fileInfo.Name))
            .ToArray();
        return saveFiles;
    }

    private static FileInfo GetOldestFileInfo(FileInfo[] saveFiles)
    {
        FileInfo oldestFile = null;
        foreach (var fileInfo in saveFiles)
        {
            if (oldestFile == null)
            {
                oldestFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime < oldestFile.LastWriteTime)
                {
                    oldestFile = fileInfo;
                }
            }
        }

        return oldestFile;
    }

    private static FileInfo GetMostRecentFileInfo(FileInfo[] saveFiles)
    {
        FileInfo mostRecentFile = null;
        foreach (var fileInfo in saveFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        }

        return mostRecentFile;
    }

    public static List<SaveFile> GetAllSaveFiles()
    {
        List<SaveFile> saveFiles = new List<SaveFile>();

        foreach (var saveFileInfo in GetAllSaveFilesInfo())
        {
            saveFiles.Add(new SaveFile()
            {
                Name = saveFileInfo.Name,
                DateTime = saveFileInfo.LastWriteTime
            });
        } 

        return saveFiles;
    }
}
