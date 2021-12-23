using System;
using UnityEngine;

public class LevelSystem : ILevelSystem
{
    private const int experienceForEnemy = 10;

    private int level;
    private int experience;
    private int experienceToNextLevel;

    private IUnitRepository UnitRepository;

    public event Action<float> ExperiencePercentChanged = percent => { };
    public event Action<int> LevelUp = level => { };

    public LevelSystem()
    {
        UnitRepository = CompositionRoot.GetUnitRepository();
        UnitRepository.UnitRemoved += OnUnitRemoved;

        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
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
}
