using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour, ISaveManager
{
    public bool IsLoading { get; private set; }

    private List<ISaveable> saveRegistry = new List<ISaveable>();
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

    public string LoadData<T>(T saveObject) where T : ISaveable
    {
        foreach (var item in currentLoadingData.Children<JProperty>())
        {
            if (item.Name.Equals(saveObject.GetType().ToString()))
            {
                return item.First.ToString(Formatting.None);
            }
        }
        return null;
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