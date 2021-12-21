using System;

public class LevelSystem : ILevelSystem
{
    private int level;
    private int experience;
    private int experienceToNextLevel;

    public event Action<float> ExperiencePercentageChanged;
    public event Action<int> LevelUp;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= experienceToNextLevel)
        {
            {
                level++;
                experience -= experienceToNextLevel;
            }
        }

    }

    public int GetLevelNumber()
    {
        return level;
    }
}
