using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class LevelSystem : MonoBehaviour, ILevelSystem
{
    private const int experienceForEnemy = 10;

    [JsonProperty]
    private int level;
    [JsonProperty]
    private int experience;
    [JsonProperty]
    private int experienceToNextLevel;

    private IUnitRepository UnitRepository;
    private ISaveManager SaveManager;

    public event Action<float> ExperiencePercentChanged = percent => { };
    public event Action<int> LevelUp = level => { };

    private void Awake()
    {
        SaveManager = CompositionRoot.GetSaveManager();
        UnitRepository = CompositionRoot.GetUnitRepository();

        UnitRepository.UnitRemoved += OnUnitRemoved;
        SaveManager.AddToSaveRegistry(this);
    }

    public void Init()
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
        ExperiencePercentChanged((float)experience / experienceToNextLevel);
    }

    public void Load(string jsonProperties)
    {
        Init();
        JObject jObject = JObject.Parse(jsonProperties);
        this.level = jObject.SelectToken("level").ToObject<int>();
        this.experience = jObject.SelectToken("experience").ToObject<int>();
        this.experienceToNextLevel = jObject.SelectToken("experienceToNextLevel").ToObject<int>();
        ExperiencePercentChanged((float)experience / experienceToNextLevel);
    }

    private void OnUnitRemoved(IAlive obj)
    {
        if (obj is IEnemy)
        {
            AddExperience(experienceForEnemy);
        }
    }

    private void AddExperience(int amount)
    {
        experience += amount;
        ExperiencePercentChanged((float)experience / experienceToNextLevel);
        if (experience >= experienceToNextLevel)
        {
            {
                level++;
                experience -= experienceToNextLevel;
                LevelUp(level);
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetExperiencePercent()
    {
        return experience;
    }

    public void PrepareSaveData() { }
}
