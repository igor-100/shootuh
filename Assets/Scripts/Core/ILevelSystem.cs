using System;

public interface ILevelSystem
{
    event Action<float> ExperiencePercentageChanged;
    event Action<int> LevelUp;
}
