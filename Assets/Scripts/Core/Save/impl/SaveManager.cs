using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour, ISaveManager
{
    private List<ISaveable> saveRegistry = new List<ISaveable>();
    private ISceneLoader SceneLoader;

    private void Awake() {
        SceneLoader = CompositionRoot.GetSceneLoader();

        SaveSystem.Init();
        DontDestroyOnLoad(this);
    }

    public void AddToSaveRegistry<T>(T saveObject) where T : ISaveable
    {
        saveRegistry.Add(saveObject);
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
        SceneLoader.LoadNextScene();

        StartCoroutine(WaitForObjectsToInitialize());
    }

    private IEnumerator WaitForObjectsToInitialize()
    {
        yield return new WaitForSeconds(0.01f);

        var json = SaveSystem.Load();
        JObject jObject = JObject.Parse(json);
        foreach (var item in jObject.Children<JProperty>())
        {
            var saveObject = saveRegistry.Find(saveObject => item.Name.Equals(saveObject.GetType().ToString()));
            if (saveObject != null)
            {
                saveObject.LoadData(item.First);
            }
            else
            {
                Debug.LogWarning(item.Name + " object was not found in save registry while loading");
            }
        }
    }
}