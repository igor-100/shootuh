using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour, ISaveManager
{
    private List<ISaveable> saveRegistry = new List<ISaveable>();

    private bool IsLoading;
    private JObject currentLoadingData;

    private void Awake()
    {
        SaveSystem.Init();
        DontDestroyOnLoad(this);
    }

    public void AddToSaveRegistry<T>(T saveObject) where T : ISaveable
    {
        saveRegistry.Add(saveObject);
    }

    public bool TryLoading<T>(T saveObject) where T : ISaveable
    {
        if (IsLoading)
        {
            foreach (var item in currentLoadingData.Children<JProperty>())
            {
                if (item.Name.Equals(saveObject.GetType().ToString()))
                {
                    saveObject.LoadData(item.First);
                    return true;
                }
            }
        }
        return false;
    }

    public void ResetSaveRegistry()
    {
        saveRegistry.Clear();
    }

    public void Save()
    {
        string[] saveObjects = new string[saveRegistry.Count];

        var count = 0;
        foreach (var saveObject in saveRegistry)
        {
            saveObject.PrepareSaveData();
            saveObjects[count] = "\"" + saveObject.GetType() + "\":" + JsonConvert.SerializeObject(saveObject);
            count++;
        }
        var jsonString = "{" + string.Join(",", saveObjects) + "}";

        SaveSystem.Save(jsonString);
    }

    public void Load()
    {
        ResetSaveRegistry();

        IsLoading = true;

        var json = SaveSystem.Load();
        currentLoadingData = JObject.Parse(json);
    }

    public void New()
    {
        ResetSaveRegistry();

        IsLoading = false;
    }
}