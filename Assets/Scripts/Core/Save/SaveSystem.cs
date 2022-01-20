using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

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
        var directoryInfo = new DirectoryInfo(saveFolder);
        Regex reg = new Regex(@"save_[0-9]+.json$");

        var saveFiles = directoryInfo.GetFiles()
            .Where(fileInfo => reg.IsMatch(fileInfo.Name))
            .ToArray();

        FileInfo oldestFile = GetOldestFileInfo(saveFiles);

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
        var directoryInfo = new DirectoryInfo(saveFolder);
        Regex reg = new Regex(@"save_[0-9]+.json$");

        var saveFiles = directoryInfo.GetFiles()
            .Where(fileInfo => reg.IsMatch(fileInfo.Name))
            .ToArray();

        FileInfo mostRecentFile = GetMostRecentFileInfo(saveFiles);

        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }
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
}